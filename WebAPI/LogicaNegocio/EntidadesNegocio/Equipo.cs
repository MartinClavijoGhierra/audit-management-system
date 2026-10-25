using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaNegocio.InterfacesEntidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.EntidadesNegocio
{
    public class Equipo: IValidable, IEquatable<Equipo?>
    {
        public int Id { get; private set; }

        public string NombreEquipo { get; set; }

        public List<Usuario> UsuariosEquipo { get; set; } = new List<Usuario>();

        private Equipo() 
        {
         
        }
        public Equipo(string nombreEquipo)
        {
            
            NombreEquipo = nombreEquipo;
            Validar();
        }
        public void Validar()
        {
            ValidarNombre();         
        }
        private void ValidarNombre()
        {
            if (string.IsNullOrEmpty(NombreEquipo))
            {
            throw new EquipoException("Debe introducir un nombre para el equipo");
            }
        }
        public override bool Equals(object? obj)
        {
            return Equals(obj as Equipo);
        }
        public bool Equals(Equipo? other)
        {
            return other is not null &&
                   Id == other.Id &&
                   NombreEquipo == other.NombreEquipo;
        }
    }
}
