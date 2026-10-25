using CasosDeUso.DTOs.PagoDTOs;
using LogicaNegocio.EntidadesNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Mappers
{
    public class MapperPago
    {



        public static PagoRecurrente PagoRecurrenteDTOToPagoRecurrente(PagoRecurrenteDTO pagoRecurrenteDTO, TipoGasto tipoGasto, Usuario usuario)
        {
            if (pagoRecurrenteDTO == null)
            {
                throw new ArgumentNullException("Datos incorrectos");
            }


            return new PagoRecurrente
            {
                FechaDesde = pagoRecurrenteDTO.FechaDesde,
                FechaHasta = pagoRecurrenteDTO.FechaHasta,
                Usuario = usuario,
                TipoGasto = tipoGasto,
                Description = pagoRecurrenteDTO.Descripcion,
                Monto = pagoRecurrenteDTO.Monto,
                MetodoPago = (MetodoPago)pagoRecurrenteDTO.MetodoPago
            };


        }



        public static PagoUnico PagoUnicoDTOToPagoUnico(PagoUnicoDTO pagoUnicoDTO, TipoGasto tipoGasto, Usuario usuario)
        {
            if (pagoUnicoDTO == null)
            {
                throw new ArgumentNullException("Datos incorrectos");
            }

            return new PagoUnico
            {
                Usuario = usuario,
                TipoGasto = tipoGasto,
                Description = pagoUnicoDTO.Descripcion,
                MetodoPago = (MetodoPago)pagoUnicoDTO.MetodoPago,
                Monto = pagoUnicoDTO.Monto,
                FechaPago = pagoUnicoDTO.FechaPago,
                NumeroRecibo = pagoUnicoDTO.NumeroRecibo,
            };
        }

        public static IEnumerable<ListadoPagoDTO> ListadoPagoToListadoPagoDTO(IEnumerable<Pago> pagos, DateTime fecha)
        {
            List<ListadoPagoDTO> listadoPagos = new List<ListadoPagoDTO>();
            foreach (Pago pago in pagos)
            {
                listadoPagos.Add(new ListadoPagoDTO()
                {
                    TipoGasto = pago.TipoGasto.Nombre,
                    UsuarionNombre = pago.Usuario.Nombre,
                    Descripcion = pago.Description,
                    MontoTotal = pago.Monto,
                    SaldoPendiente = pago.CalcularMontoPendiente(fecha)
                    
                });
            }
            return listadoPagos;
        }

       

        public static ListadoPagoDTO PagoToListadoPagoDTO(Pago pago)
        {

            // Verificar si el pago realmente es de tipo PagoUnico
            if (pago == null)
            {
                throw new ArgumentNullException("El pago no se ha encontrado");
            }

            return new ListadoPagoDTO()
            {
                Id = pago.Id,
                TipoGasto = pago.TipoGasto.Nombre,
                UsuarionNombre = pago.Usuario.Nombre,
                Descripcion = pago.Description,
                MontoTotal = pago.CalcularMontoTotal(),
                SaldoPendiente = pago.CalcularMontoPendiente(DateTime.Now),
                
            };
        }

        public static IEnumerable<ListadoPagoDTO> PagoToObtenerPagoDTO(IEnumerable<Pago> pagos)
        {
            List<ListadoPagoDTO> listadoPagos = new List<ListadoPagoDTO>();
            foreach (Pago pago in pagos)
            {
                listadoPagos.Add(new ListadoPagoDTO()
                {
                    Id=pago.Id,
                    TipoGasto = pago.TipoGasto.Nombre,
                    UsuarionNombre = pago.Usuario.Nombre,
                    Descripcion = pago.Description,
                    MontoTotal = pago.Monto,
                    SaldoPendiente = pago.CalcularMontoPendiente(DateTime.Now)
                });
            }
            return listadoPagos;

        }
    }
}
