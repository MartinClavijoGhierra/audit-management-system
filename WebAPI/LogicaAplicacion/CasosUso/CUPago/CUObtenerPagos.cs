using CasosDeUso.DTOs.PagoDTOs;
using CasosDeUso.InterfacesCU.CUPago;
using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaAplicacion.Mappers;
using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUPago
{
    public class CUObtenerPagos : ICUObtenerPagos
    {
        public IRepositorioPago RepoPago { get; set; }
        public CUObtenerPagos (IRepositorioPago repoPago)
        {
            RepoPago = repoPago;
        }

        public IEnumerable<ListadoPagoDTO> Ejecutar(int id)
        {
            if(id > 0)
            {
                IEnumerable<Pago> pagos = RepoPago.FindPagosById(id);
                return MapperPago.PagoToObtenerPagoDTO(pagos);

            }
            else
            {
                throw new PagoException("El id ingresado no es correcto");
            }
        }
    }
}
