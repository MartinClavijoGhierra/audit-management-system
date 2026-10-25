using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.Repositorios
{
    public class RepositorioPagoEF : IRepositorioPago
    {
        public EmpresaContexto Contexto { get; set; }
        public RepositorioPagoEF(EmpresaContexto contexto)
        {
            Contexto = contexto;
        }
        public void Add(Pago item)
        {
            item.Validar();
            Contexto.Pagos.Add(item);
            Contexto.SaveChanges();
        }

        public void Delete(Pago item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Pago> FindAll()
        {
            return Contexto.Pagos;
        }


        public IEnumerable<Pago> FindPagosById(int id)
        {
            return Contexto.Pagos
                .Include(p => p.TipoGasto)
                .Include(p => p.Usuario)
                .Where(p => p.Usuario.Id == id)
                .ToList();

        }


        public Pago FindById(int id)
        {
            return Contexto.Pagos
                .Include(p => p.Usuario)
                .Include(p => p.TipoGasto)
                .FirstOrDefault(p => p.Id == id);
        }

        public void Update(int id, Pago item)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<Usuario> UsuariosMayorPago(decimal montoEntrada)
        {
            return Contexto.Pagos
                .OfType<PagoUnico>()                       // Solo pagos de tipo PagoUnico
                .Where(p => p.Monto > montoEntrada)        // Filtra los que superan el monto
                .Include(p => p.Usuario)                    // Carga el usuario
                  .ThenInclude(u => u.Equipo)           // Carga el equipo del usuario
                .Include(p => p.Usuario)
                  .ThenInclude(u => u.Rol)              // Carga el rol del usuario
                .Select(p => p.Usuario)                    // Obtiene el usuario de cada pago
                .Distinct()                                // Evita duplicados
                .ToList();
        }

        //PARA RESOLVER EN CLASE
        public IEnumerable<Pago> FindByMes(int mes, int anio)
        {
            DateTime fechaReferencia = new DateTime(anio, mes, 1);

            // Pagos únicos
            var pagosUnicos = Contexto.Pagos
                .OfType<PagoUnico>()
                .Where(pu => pu.FechaPago.Year == anio && pu.FechaPago.Month == mes)
                .Include(p => p.Usuario)
                .Include(p => p.TipoGasto)
                .ToList();

            // Pagos recurrentes
            var pagosRecurrentes = Contexto.Pagos
                .OfType<PagoRecurrente>()
                .Where(pr => pr.FechaDesde <= fechaReferencia && pr.FechaHasta >= fechaReferencia)
                .Include(p => p.Usuario)
                .Include(p => p.TipoGasto)
                .ToList();

            // Combinar ambos resultados
            return pagosUnicos.Cast<Pago>().Concat(pagosRecurrentes.Cast<Pago>());
        }

        public IEnumerable<Equipo> ListadoPorMonto(decimal monto)
        {
            return Contexto.Pagos
            .OfType<PagoUnico>()                  // Solo pagos únicos
            .Where(p => p.Monto > monto)          // Filtra los que superan el monto dado
            .Include(p => p.Usuario)              // Incluye el usuario
                .ThenInclude(u => u.Equipo)       // e incluye el equipo del usuario
            .Select(p => p.Usuario.Equipo)        // Obtiene el equipo asociado al usuario
            .Distinct()                           // Evita equipos repetidos
            .OrderByDescending(e => e.NombreEquipo)     // Ordena por nombre descendente
            .ToList();                            // Devuelve la lista


        }
    }
}
