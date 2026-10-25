using CasosDeUso.InterfacesCU.CUTipoGastos;
using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUTipoGasto
{
    public class CUEliminarGasto : ICUEliminarGasto
    {
        public IRepositorioTipoGasto RepoTipoGasto { get; set; }
        public IRepositorioPago RepoPago { get; set; }

        public CUEliminarGasto(IRepositorioTipoGasto repoTipoGasto, IRepositorioPago repoPago ) {
        
            RepoTipoGasto = repoTipoGasto;
            RepoPago = repoPago;
        }


        public void Ejecutar(int id)
        {

            
            bool activo=false;
            TipoGasto gasto = RepoTipoGasto.FindById(id);

            if (gasto == null) 
                {

                throw new TipoGastoException("Tipo de gasto no disponible");


            }


            IEnumerable<Pago> Pagos = RepoPago.FindAll();

            foreach (Pago pago in Pagos)
            {
                if (pago.TipoGasto != null && pago.TipoGasto.Id == id)
                {
                    throw new TipoGastoException("El gasto se encuentra activo. No es posible eliminarlo");
                }
            }
            

            if(!activo)
            {
                RepoTipoGasto.Delete(gasto);
            }
            
        }
    }
}
