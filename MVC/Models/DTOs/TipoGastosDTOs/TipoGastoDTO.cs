using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs.TipoGastosDTOs
{
    public class TipoGastoDTO
    {

        [Display(Name = "Nombre del Gasto:")]
        [Required(ErrorMessage = "Debe ingresar Nombre del Gasto.")]
        public string Nombre { get; set; }

        [Display(Name = "Descripción del Gasto:")]
        [Required(ErrorMessage = "Debe ingresar Descripción del Gasto.")]
        public string Description { get; set; }
        public bool Activo { get; set; }
        

    }
}
