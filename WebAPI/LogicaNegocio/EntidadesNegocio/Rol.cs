using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaNegocio.InterfacesEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.EntidadesNegocio
{
    public class Rol : IValidable
    {
        public int Id { get; set; }
        public string NombreRol { get; set; }

        private Rol()
        {
           
        }
        public Rol(string nombreRol)
        {
            NombreRol = nombreRol;
            Validar();
        }

        public void Validar()
        {
            //HAY QUE VALIDAR QUE SEA ADMINISTRADOR, GERENTE O USUARIO, SOLO ESOS TIPOS
            if(string.IsNullOrEmpty(NombreRol))
            {
                throw new RolException("Debe ingresar un tipo de Rol");

             }
        }
    }

    
}
