using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUso.DTOs.UsuarioDTOs
{
    public class UsuarioLogueadoDTO

    {

        //VA A SER EL OBJETO QUE DEVUELVO UNA VEZ SE LOGUEA EL USUARIO, 
        //FIJAREMQ UE DATOS DOY
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public int Rol { get; set; }
        public string RolString {  get; set; }
        public string Token { get; set; }


    }
}
