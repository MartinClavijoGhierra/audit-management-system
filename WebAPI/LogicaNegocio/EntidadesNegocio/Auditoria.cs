using ExcepcionesPropias.ExcepcionesEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.EntidadesNegocio
{
    public class Auditoria
    {
        public int Id { get; private set; }      
        public Usuario? Usuario { get; set; }
        public string Accion {get; set;}
        public DateTime Fecha { get; set; }
        public TipoGasto? TipoGasto { get; set; }
        public bool Activo { get; set; }

        private Auditoria() 
        {
        
        }
        public Auditoria(Usuario usuario, string accion, TipoGasto tipogasto, bool activo)
        {

            Usuario = usuario;
            Accion = accion;
            TipoGasto = tipogasto;
            Fecha = DateTime.Now;
            Validar();
            Activo = activo;
        }

        public void Validar()
        {
            if (Usuario == null) 
            {
                throw new AuditoriaException("El usuario no existe");             
            }
                      
        }
        
    }
}
