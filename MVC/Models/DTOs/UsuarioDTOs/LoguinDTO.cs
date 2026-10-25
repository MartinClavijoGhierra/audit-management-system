using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs.UsuarioDTOs
{
    public class LoguinDTO
    {
        [Display(Name = "Ingrese su Email")]
        [EmailAddress(ErrorMessage = "Debe ingresar un correo válido")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Debe ingresar su Email.")]

        public string Email { get; set; }

        [Display(Name = "Ingrese su Contraseña")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Debe ingresar su pasword.")]
        public string Contrasenia { get; set; }

    }
}
