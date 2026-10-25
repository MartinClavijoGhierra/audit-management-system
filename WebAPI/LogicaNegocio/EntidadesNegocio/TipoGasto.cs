using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaNegocio.InterfacesEntidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.EntidadesNegocio
{
    public class TipoGasto : IValidable
    {
        public int Id { get; private set; }
        public string Nombre { get; set; }
        public string Description { get; set; }
        public bool Activo { get; set; }
        private TipoGasto()
        {
        }
        public TipoGasto(string nombre, string description)
        {

            Nombre = nombre;
            Description = description;
            Activo = true;
            Validar();
        }
        public void Validar()
        {
            ValidarNombre();
            ValidarDescription();
        }
        private void ValidarDescription()
        {
            if (string.IsNullOrEmpty(Description))
            {
            throw new TipoGastoException("Es necesario ingresar la descripcion del Tipo de Gasto");

            }
        }
        private void ValidarNombre()
        {

            if (string.IsNullOrEmpty(Nombre))
            {

            throw new TipoGastoException("Debe ingresar un Nombre para el Tipo de Gasto");
            }
        }
    }
}
