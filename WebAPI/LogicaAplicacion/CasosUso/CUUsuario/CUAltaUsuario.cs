using CasosDeUso.DTOs.UsuarioDTOs;
using CasosDeUso.InterfacesCU.CUUsuario;
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
    public class CUAltaUsuario : ICUAltaUsuario
    {
        public IRepositorioUsuario RepoUsuario {  get; set; }
        public IRepositorioEquipo RepoEquipo { get; set; }
        public IRepositorioRol RepoRol { get; set; }
        public CUAltaUsuario(IRepositorioUsuario repoUsuario,
            IRepositorioEquipo repoEquipo, IRepositorioRol repoRol) 
        { 
            RepoUsuario = repoUsuario;
            RepoEquipo = repoEquipo;
            RepoRol = repoRol;
        }

        public void Ejecutar(UsuarioDTO usuarioDTO)
        {
            Rol rol = RepoRol.FindById(usuarioDTO.Rol);
            Equipo equipo = RepoEquipo.FindById(usuarioDTO.EquipoId);
            Usuario usuario = MapperUsuario.UsuarioDTOToUsuario(usuarioDTO, equipo, rol);
            RepoUsuario.Add(usuario);
        }
    }
}
