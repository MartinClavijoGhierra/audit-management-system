using CasosDeUso.DTOs.RolDTOs;
using LogicaNegocio.EntidadesNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Mappers
{
    public class MapperRol
    {
        public static IEnumerable<ListadoRolDTO> ListadoRolesToListadoRolesDTO(IEnumerable<Rol> roles)
        {
            List<ListadoRolDTO> listadoRoles = new List<ListadoRolDTO>();
            foreach(Rol rol in roles)
            {
                if(rol.NombreRol != "Administrador")
                {

                    listadoRoles.Add(new ListadoRolDTO()
                    {
                        Id = rol.Id,
                        NombreRol = rol.NombreRol,
                    });
                }
            }
            return listadoRoles;
        }
    }
}
