using System.ComponentModel.DataAnnotations;

namespace MVC.Models.DTOs.AuditoriaDTOs
{
    public class ListadoAuditoriaDTO
    {
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime Fecha { get; set; }
        public string Accion { get; set; }
        public string TipoGasto { get; set; }
        public int UsuarioId { get; set; }
        public string UsuarioName { get; set; }
        public bool Activo { get; set; }
    }
}
