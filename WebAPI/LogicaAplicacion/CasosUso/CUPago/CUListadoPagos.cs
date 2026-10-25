using CasosDeUso.DTOs.PagoDTOs;
using CasosDeUso.InterfacesCU.CUPago;
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
    public class CUListadoPagos:ICUListadoPago
    {
        public IRepositorioPago RepoPago { get; set; }
        public CUListadoPagos(IRepositorioPago repoPago)
        {
            RepoPago = repoPago;
        }

        public IEnumerable<ListadoPagoDTO> Ejecutar(int mes, int anio)
        {
            IEnumerable<Pago> Pagos = RepoPago.FindByMes(mes, anio);
            DateTime fechaReferencia = new DateTime(anio, mes, 1);
            return MapperPago.ListadoPagoToListadoPagoDTO(Pagos, fechaReferencia);
        }
    }
}
