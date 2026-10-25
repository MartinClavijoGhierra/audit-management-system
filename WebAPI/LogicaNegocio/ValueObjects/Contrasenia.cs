using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaNegocio.InterfacesEntidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.ValueObjects
{
    [Owned]
    public class Contrasenia : IValidable
    {
        public string Valor {  get; set; }

        public Contrasenia(string valor) 
        {
            Valor = valor;
            Validar();
        }

        public void Validar()//que sea mayor = a 9 caracteres
        {
            ValidarNullOrEmpty();
            ValidarCaracteres();
        }


        private void ValidarNullOrEmpty()
        {
            if (string.IsNullOrEmpty(Valor))
            {

                throw new UsuarioException("La contraseña debe ser completada.");
            }

        }
        private void ValidarCaracteres()
        {
            if (Valor.Length < 9)
            {

                throw new UsuarioException("La contraseña debe tener al menos 9 caracteres.");
            }
        
        }

        public string GenerarNuevaContrasenia()
        {
            const string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < 9; i++) // longitud mínima
            {
                sb.Append(caracteres[random.Next(caracteres.Length)]);
            }

            return sb.ToString();
        }
    }
}
