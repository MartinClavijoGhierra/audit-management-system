using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaNegocio.InterfacesEntidades;
using LogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.EntidadesNegocio
{
    public class Usuario : IValidable, IEquatable<Usuario?>
    {
        public int Id { get;  set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public Equipo Equipo { get; set; }
        public Contrasenia Contrasenia { get; set; }
        public string Email { get;  set; }
        public Rol Rol { get;  set; }
        private Usuario()
        {         
        }

        //modifico constructor para que no me de error
        public Usuario(string nombre, string apellido, Equipo equipo, string contrasenia,  Rol rol)
        {
            
            Nombre = nombre;
            Apellido = apellido;
            Equipo = equipo;
            Contrasenia = new Contrasenia(contrasenia);
            Rol = rol;
            Email = GenerarEmailBase(nombre, apellido);
            Validar();

        }

        public string GenerarEmailBase(string nombre, string apellido)
        {
            string parteNombre = nombre.Length > 3 ? nombre.Substring(0, 3) : nombre;
            string parteApellido = apellido.Length > 3 ? apellido.Substring(0, 3) : apellido;

            parteNombre = ReemplazarCaracteresEspeciales(parteNombre.ToLower());
            parteApellido = ReemplazarCaracteresEspeciales(parteApellido.ToLower());

            return parteNombre + parteApellido + "@laempresa.com";
        }

        // Reemplazo de tildes y ñ (igual que antes)
        private string ReemplazarCaracteresEspeciales(string texto)
        {
            texto = texto.Replace("á", "a").Replace("é", "e").Replace("í", "i")
                         .Replace("ó", "o").Replace("ú", "u")
                         .Replace("Á", "a").Replace("É", "e").Replace("Í", "i")
                         .Replace("Ó", "o").Replace("Ú", "u")
                         .Replace("ñ", "n").Replace("Ñ", "n");
            return texto;
        }


        public void Validar()
        {
            //TODO TODOS 
            ValidarNombre();
            ValidarApellido();
            ValidarEquipoVacio();

        }
        private void ValidarApellido()
        {
            if (string.IsNullOrEmpty(Apellido))
            {
                throw new UsuarioException("Debe ingresar el Apellido");
            }
        }
        private void ValidarEquipoVacio()
        {
            if (Equipo == null)
            {
                throw new UsuarioException("El usuario debe tener un equipo asignado");
            }
        }
        private void ValidarNombre()
        {
            if (string.IsNullOrEmpty(Nombre))
            {
                throw new UsuarioException("Debe ingresar el Nombre");
            }
        }
        public bool Equals(Usuario? other)
        {
            return Email.Equals(other.Email);
        }
    }
 }
