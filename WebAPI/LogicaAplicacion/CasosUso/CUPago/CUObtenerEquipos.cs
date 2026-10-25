using CasosDeUso.DTOs.EquipoDTOs;
using CasosDeUso.InterfacesCU.CUEquipo;
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
    public class CUObtenerEquipos : ICUEquiposMonto
    {
        public IRepositorioPago RepoPago { get; set; }
        public CUObtenerEquipos(IRepositorioPago repoPago)
        {
            RepoPago = repoPago;
        }

        public IEnumerable<ListadoEquipoDTO> Ejecutar(decimal monto)
        {
            if(monto <= 0)
            {
                throw new PagoException("Debe ingresar un monto superior a 0");
            }
            else
            {
                IEnumerable<Equipo> equipos = RepoPago.ListadoPorMonto(monto);
                return MapperEquipo.ListadoEquiposToListadoEquiposDTO(equipos);

            }
        }
    }
}
