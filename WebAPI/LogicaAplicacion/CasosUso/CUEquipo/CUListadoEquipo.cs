using CasosDeUso.DTOs.EquipoDTOs;
using CasosDeUso.InterfacesCU.CUEquipo;
using LogicaAplicacion.Mappers;
using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUEquipo
{
    public class CUListadoEquipo : ICUListadoEquipo
    {
        public IRepositorioEquipo RepoEquipo { get; set; }
        public CUListadoEquipo(IRepositorioEquipo repoEquipo)
        {
            RepoEquipo = repoEquipo;
        }
        public IEnumerable<ListadoEquipoDTO> Ejecutar()
        {
            IEnumerable<Equipo> Equipos = RepoEquipo.FindAll();
            return MapperEquipo.ListadoEquiposToListadoEquiposDTO(Equipos);
        }
    }
}
