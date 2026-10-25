using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs.TipoGastosDTOs
{
    public class ListadoTipoGastoDTO
    {
        public int Id { get; set; }

        [Display(Name = "Nombre del gasto:")]
        public string Nombre { get; set; }

        [Display(Name = "Descripcion del gasto:")]
        public string Description { get; set; }

        [Display(Name = "Estado:")]
        public bool Activo {  get; set; }
    }
}
