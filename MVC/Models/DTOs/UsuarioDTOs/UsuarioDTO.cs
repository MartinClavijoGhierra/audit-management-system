using Models.DTOs.EquipoDTOs;
using Models.DTOs.RolDTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs.UsuarioDTOs
{
    public class UsuarioDTO
    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Debe indicar cual es el Nombre del Usuario")]
        public string Nombre { get; set; }


        [Required(ErrorMessage = "Debe indicar cual es el Apellido del Usuario")]
        public string Apellido { get; set; }



        [Display(Name = "Seleccione un equipo")]
        [Required(ErrorMessage = "Debe seleccionar una opción")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una opción válida")]
        public int EquipoId { get; set; }


        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres")]
        [DataType(DataType.Password)]
        public string Contrasenia { get; set; }

        [Display(Name = "Rol del Usuario nuevo")]
        [Required(ErrorMessage = "Debe seleccionar una opción")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una opción válida")]
        public int Rol {  get; set; }
        public IEnumerable<ListadoEquipoDTO> Equipos { get; set; } = new List<ListadoEquipoDTO>();

        public IEnumerable<ListadoRolDTO> Roles { get; set; } = new List<ListadoRolDTO>();

    }
}
