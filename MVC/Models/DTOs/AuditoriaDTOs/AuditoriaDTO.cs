using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs.AuditoriaDTOs
{
    public class AuditoriaDTO
    {

        public DateTime Fecha { get; set; } = DateTime.Now;
        
        public string Accion {  get; set; } 
        public int TipoGasto { get; set; }
        public int UsuarioId { get; set; }
        public bool Activo { get; set; }
    }
}
