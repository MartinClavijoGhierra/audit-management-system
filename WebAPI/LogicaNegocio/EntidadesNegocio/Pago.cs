using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaNegocio.InterfacesEntidades;
using LogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.EntidadesNegocio
{
    public abstract class Pago : IValidable
    {
        public int Id { get; private set; }

       

        public MetodoPago MetodoPago { get; set; }
        public Usuario Usuario { get; set; }

        [Display(Name = "Tipo de Gasto")]
        public TipoGasto TipoGasto { get; set; }

        [Display(Name = "Descripcion del pago")]
        public string Description { get; set; }

        [Display(Name = "Monto del pago")]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "El monto no puede ser menor a 0")]
        public decimal Monto { get; set; }

        protected Pago()
        {        
        }
        public Pago(MetodoPago metodoPago, Usuario usuario, TipoGasto tipoGasto, string description, decimal monto)
        {
            
            MetodoPago = metodoPago;
            Usuario = usuario;
            TipoGasto = tipoGasto;
            Description = description;
            Monto = monto;
            Validar();
        }
        //TODO
        public abstract decimal CalcularMontoTotal();

        public abstract decimal CalcularMontoPendiente(DateTime fecha);
        public void Validar()
        {
            ValidarDescription();
            ValidarMonto();
        }
        private void ValidarMonto()
        {
            if (Monto <= 0)
            {
                throw new PagoException("El monto no puede ser menor a 0");

            }
        }
        private void ValidarDescription()
        {
            if (string.IsNullOrEmpty(Description))
            {
                throw new PagoException("Es necesario ingresar una descripcion del pago");
            }
           
        }
        
    }
}
