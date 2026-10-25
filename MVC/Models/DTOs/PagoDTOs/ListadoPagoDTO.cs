using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs.PagoDTOs
{
    public class ListadoPagoDTO
    {
        [Display(Name = "Id Pago")]
        public int Id { get; set; }

        [Display(Name = "Tipo de gasto:")]
        public string TipoGasto { get; set; }

        [Display(Name = "Nombre de usuario:")]
        public string UsuarionNombre { get; set; }

        [Display(Name = "Descripcion:")]
        public string Descripcion { get; set; }

        [Display(Name = "Monto Total del pago:")]
        public decimal MontoTotal { get; set; }

        [Display(Name = "Saldo Pendiente:")]
        public decimal SaldoPendiente { get; set; }
    }
}
