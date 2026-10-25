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
    public class CUBuscarPago : ICUBuscarPago
    {

        public IRepositorioPago RepoPago { get; set; }
        public CUBuscarPago(IRepositorioPago repoPago)
        {
            RepoPago = repoPago;
        }
        public ListadoPagoDTO Ejecutar(int id)
        {
            Pago pago = RepoPago.FindById(id);
            if (pago != null)
            {
                return MapperPago.PagoToListadoPagoDTO(pago);
            }
            else
            {
                throw new PagoException("No se ha encontrado un pago con el ID indicado");
            }
        }
    }
}
