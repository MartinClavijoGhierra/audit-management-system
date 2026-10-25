using Models.DTOs.TipoGastosDTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs.PagoDTOs
{
    public class PagoUnicoDTO
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


        //especifico pago unico

        [Display(Name = "Indique la fehca de pago:")]
        [Required(ErrorMessage = "Debe indicar la fecha de pago")]
        public DateTime FechaPago { get; set; }

        [Display(Name = "Numero de Recibo:")]
        [Range(1, int.MaxValue, ErrorMessage = "El monto debe ser un número entero mayor a 0")]
        [Required(ErrorMessage = "Debe indicar numero de Recibo")]
        public int NumeroRecibo { get; set; }


    }
}
