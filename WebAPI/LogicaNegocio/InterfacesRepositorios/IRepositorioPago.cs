using LogicaNegocio.EntidadesNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepositorios
{
    public interface IRepositorioPago : IRepositorio<Pago>
    {
        IEnumerable<Pago> FindByMes(int mes, int anio);
        IEnumerable<Usuario> UsuariosMayorPago(decimal monto);
        IEnumerable<Pago> FindPagosById(int id);

        IEnumerable<Equipo> ListadoPorMonto(decimal monto);
    }
}
