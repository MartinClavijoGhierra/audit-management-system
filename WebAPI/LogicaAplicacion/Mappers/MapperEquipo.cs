using CasosDeUso.DTOs.EquipoDTOs;
using LogicaNegocio.EntidadesNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Mappers
{
    public class MapperEquipo
    {
        public static IEnumerable<ListadoEquipoDTO> ListadoEquiposToListadoEquiposDTO(IEnumerable<Equipo> equipos)
        {
            List<ListadoEquipoDTO> listadoEquipos = new List<ListadoEquipoDTO>();
            foreach(Equipo equipo in equipos)
            {
                listadoEquipos.Add(new ListadoEquipoDTO()
                {
                    Id = equipo.Id,
                    Nombre = equipo.NombreEquipo,
                });
            }
            return listadoEquipos;
        }
    }
}
