using CasosDeUso.DTOs.RolDTOs;
using CasosDeUso.InterfacesCU.CURol;
using LogicaAplicacion.Mappers;
using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CURol
{
    public class CUListadoRol : ICUListadoRol
    {
        public IRepositorioRol RepoRol { get; set; }
        public CUListadoRol (IRepositorioRol repoRol)
        { 
            RepoRol = repoRol; 
        }

        public IEnumerable<ListadoRolDTO> Ejecutar()
        {
            IEnumerable<Rol> Roles = RepoRol.FindAll();
            return MapperRol.ListadoRolesToListadoRolesDTO(Roles);
        }
    }
}
