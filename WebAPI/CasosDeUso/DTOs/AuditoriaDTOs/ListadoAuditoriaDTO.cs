using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUso.DTOs.AuditoriaDTOs
{
    public class ListadoAuditoriaDTO
    {

        public DateTime Fecha { get; set; } 
        public string Accion { get; set; }
        public string TipoGasto { get; set; }
        public int UsuarioId { get; set; }
        public string UsuarioName { get; set; }
        public bool Activo { get; set; }


    }
}

