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
    public class CUObtenerUsuarios : ICUUsuarioPago
    {
        public IRepositorioUsuario RepoUsuario { get; set; }
        public IRepositorioPago RepoPago { get; set; }

        public CUObtenerUsuarios(IRepositorioUsuario repoUsuario,
            IRepositorioPago repoPago)
        {
            RepoUsuario = repoUsuario;
            RepoPago = repoPago;
        }

        public IEnumerable<ObtenerUsuarioDTO> Ejecutar(decimal monto)
        {
            IEnumerable<Usuario> Usuarios = RepoPago.UsuariosMayorPago(monto);
            return MapperUsuario.ListUsuarioToListUsuarioDTOS(Usuarios);
        }
    }
}
