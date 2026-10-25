using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUso.DTOs.PagoDTOs
{
    public class ListadoPagoDTO
    {
        public int Id { get; set; }

        [Display(Name = "Tipo de gasto:")]
        public string TipoGasto { get; set; }

        [Display(Name = "Nombre de usaurio:")]
        public string UsuarionNombre { get; set; }

        [Display(Name = "Descripcion:")]
        public string Descripcion { get; set; }

        [Display(Name = "Monto Total del pago:")]
        public decimal MontoTotal { get; set; }

        [Display(Name = "Saldo Pendiente:")]
        public decimal SaldoPendiente { get; set; }
    }
}
