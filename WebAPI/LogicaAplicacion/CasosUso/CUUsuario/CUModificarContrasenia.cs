using CasosDeUso.DTOs.TipoGastosDTOs;
using CasosDeUso.InterfacesCU.CUUsuario;
using ExcepcionesPropias.ExcepcionesEntidades;
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
    public class CUModificarContrasenia : ICUModificarContrasenia
    {
        public IRepositorioUsuario RepoUsuario { get; set; }
        public CUModificarContrasenia (IRepositorioUsuario repoUsuario)
        {
            RepoUsuario = repoUsuario;
        }


        public string Ejecutar(string email)
        {
            if(email != null)
            {
                string contrasenia = RepoUsuario.ModificarContrasenia(email);
                return contrasenia;
                
            }
            else
            {
                throw new UsuarioException("No se ingreso contrasenia");
            }
        }

    }
}
