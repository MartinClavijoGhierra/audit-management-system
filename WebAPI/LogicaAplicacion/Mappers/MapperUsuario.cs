using CasosDeUso.DTOs.UsuarioDTOs;
using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Mappers
{
    public class MapperUsuario
    {

        //METODO PARA EL ALTA, RECIBO DTO Y GUARDO USUARIO

        public static Usuario UsuarioDTOToUsuario(UsuarioDTO usuarioDTO, Equipo equipo, Rol rol)
        {

            return new Usuario(usuarioDTO.Nombre, usuarioDTO.Apellido,
                equipo, usuarioDTO.Contrasenia, rol);
 
        }

        //METODO PARA LISTAR, DEVUELO LISTA DTOs



        //METODO PARA LOGUEO, DEVUELVO USUARIO DTO
        public static UsuarioLogueadoDTO UsuarioToUsuarioLogueadoDTO(Usuario usuario)
        {
            return new UsuarioLogueadoDTO
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Email = usuario.Email,
                Rol = usuario.Rol.Id,
                RolString = usuario.Rol.NombreRol

                //!= null ? usuario.Rol.Id : 0,  // o -1 si quieres indicar "sin rol"
            };
        }

        public static IEnumerable<ObtenerUsuarioDTO> ListUsuarioToListUsuarioDTOS(IEnumerable<Usuario> usuarios)
        {
            List<ObtenerUsuarioDTO> listadoUsuarios = new List<ObtenerUsuarioDTO>();

            foreach (Usuario usuario in usuarios)
            {
                listadoUsuarios.Add(new ObtenerUsuarioDTO()
                {
                    Id = usuario.Id,
                    Name = usuario.Nombre,
                    Apellido = usuario.Apellido,
                    Equipo = usuario.Equipo.NombreEquipo, 
                    Email = usuario.Email,
                    Rol = usuario.Rol.NombreRol 
                });
            }

            return listadoUsuarios;
        }
    }
}
