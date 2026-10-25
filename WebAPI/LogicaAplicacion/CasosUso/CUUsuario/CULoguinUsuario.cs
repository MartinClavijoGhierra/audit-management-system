using CasosDeUso.DTOs.UsuarioDTOs;
using CasosDeUso.InterfacesCU.CUUsuario;
using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaAplicacion.CasosUso.CUUsuario;
using LogicaAplicacion.Mappers;
using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUUsuario
{
    public class CULoguinUsuario : ICULoguinUsuario
    {
        public IRepositorioUsuario RepoUsuario { get; set; }
        

        public CULoguinUsuario (IRepositorioUsuario repoUsuario)
        {
            RepoUsuario = repoUsuario;
            
        }

        public UsuarioLogueadoDTO Ejecutar(LoguinDTO loguinDTO)
        {
            Usuario usuario = RepoUsuario.Login(loguinDTO.Email, loguinDTO.Contrasenia);
            if(usuario == null)
            {
                throw new UsuarioException("No se ha encontrado un usuario con estas credenciales. Intente nuevamente o contactese con el Administrador.");
            }
            return MapperUsuario.UsuarioToUsuarioLogueadoDTO(usuario);
        }
    }
}

