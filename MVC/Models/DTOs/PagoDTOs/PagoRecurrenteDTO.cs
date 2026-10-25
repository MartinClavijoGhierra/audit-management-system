using Models.DTOs.TipoGastosDTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs.PagoDTOs
{
    public class PagoRecurrenteDTO
    {
        [Display(Name = "Id Usuario")]
        public int UsuarioId { get; set; }


        [Display(Name = "Metodo de Pago")]
        [Required(ErrorMessage = "Debe ingresar metodo de pago.")]
        public int MetodoPago { get; set; }

        [Display(Name = "Seleccione el gasto")]
        [Required(ErrorMessage = "Debe seleccionar una opción")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una opción válida")]
        public int TipoGastoID { get; set; } 
        public IEnumerable<ListadoTipoGastoDTO> Gasto { get; set; } = new List<ListadoTipoGastoDTO>();

        [Display(Name = "Descripcion del pago:")]
        [Required(ErrorMessage = "Debe ingresar descripcion del pago.")]
        public string Descripcion { get; set; }


        //aca en el monto hay que ver si ponemos el total, o la cuota

        [Display(Name = "Monto total:")]
        [Column(TypeName = "decimal(18,2)")]
        [Range(1, double.MaxValue, ErrorMessage = "El monto no puede ser menor a 0")]
        public decimal Monto { get; set; }


        //especifico pago Recurrente
        [Display(Name = "Indique la fehca de inicio de pago:")]
        [Required(ErrorMessage = "Debe indicar la fecha de pago")]
        public DateTime FechaDesde { get; set; }

        [Display(Name = "Indique la fehca final de pago:")]
        [Required(ErrorMessage = "Debe indicar la fecha de cierre del pago")]
        public DateTime FechaHasta { get; set; }




    }
}
