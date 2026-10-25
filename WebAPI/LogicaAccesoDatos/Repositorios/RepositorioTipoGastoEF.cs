using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.Repositorios
{
    public class RepositorioTipoGastoEF : IRepositorioTipoGasto
    {
        public EmpresaContexto Contexto { get; set; }

        public RepositorioTipoGastoEF(EmpresaContexto contexto)
        {
            Contexto = contexto;
        }

        public void Add(TipoGasto tipoGasto)
        {
            tipoGasto.Validar();
            if (!Contexto.TiposGastos.Contains(tipoGasto))
            {
                Contexto.TiposGastos.Add(tipoGasto);
                Contexto.SaveChanges();
            }
            else
            {
                throw new TipoGastoException("Usuario ya se encuentra registrado");
            }
        }

        public void Delete(TipoGasto item)
        {
            Contexto.TiposGastos.Remove(item);
            Contexto.SaveChanges();
        }

        public IEnumerable<TipoGasto> FindAll()
        {
            return Contexto.TiposGastos;
        }

        public TipoGasto FindById(int id)
        {
            return Contexto.TiposGastos
                .Where(c => c.Id == id)
                .SingleOrDefault();
            /*return Contexto.TiposGastos.Find(id);*/
        }

        public void Update(int id, TipoGasto item)
        {
           item.Validar();
            TipoGasto gasto = FindById(id);
            if(gasto != null)
            {
                gasto.Nombre = item.Nombre;
                gasto.Description = item.Description;
                gasto.Activo = item.Activo;
                Contexto.SaveChanges();
            }
            else
            {
                throw new TipoGastoException("No se encontro cliente ");
            }
        }


        //CAMBIO EL ESTADO DE LA ACTIVIDAD
        //ESTO SE PUEDE HACER DESDE EL EDIT, 
        //EVALUAR DE DEJARLO
        /*public void Baja(int id)
        {
            TipoGasto gasto = FindById(id);
            if (gasto != null)
            {

                gasto.Activo = false;
                Contexto.SaveChanges();
            }
            else
            {
                throw new TipoGastoExeption("No se encontro cliente ");
            }
        }*/
    }
}
