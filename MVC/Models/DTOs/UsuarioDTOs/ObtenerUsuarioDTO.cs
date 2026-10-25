using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs.UsuarioDTOs
{
    public class ObtenerUsuarioDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Apellido { get; set; }
        public string Equipo { get; set; }
        public string Email { get; set; }
        public string Rol { get; set; }

    }
}