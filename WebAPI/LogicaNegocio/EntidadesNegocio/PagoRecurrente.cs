using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaNegocio.InterfacesEntidades;
using LogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.EntidadesNegocio
{
    public class PagoRecurrente : Pago, IValidable
    {
        public DateTime FechaDesde {get;set;}
        public DateTime FechaHasta { get; set; }



        public PagoRecurrente(): base()
        {
            
        }
        public PagoRecurrente(DateTime fechaDesde, DateTime fechaHasta, 
            MetodoPago metodoPago, Usuario usuario, TipoGasto tipoGasto, 
            string description, decimal monto) : base(metodoPago, usuario, tipoGasto, description, monto)
        {
            FechaDesde = fechaDesde;
            FechaHasta= fechaHasta;
            Validar();
        }

        public void Validar()
        {
            //TODO
        }

        public override decimal CalcularMontoTotal()
        {
            int cantidadMeses = ((FechaHasta.Year - FechaDesde.Year) * 12) +
                FechaHasta.Month - FechaDesde.Month + 1;

            return base.Monto * cantidadMeses;
        }



        public override decimal CalcularMontoPendiente(DateTime fecha)
        {
            if (fecha < FechaDesde) return CalcularMontoTotal(); // todavía no empezó
            if (fecha > FechaHasta) return 0; // ya terminó

            int mesesTotales = ((FechaHasta.Year - FechaDesde.Year) * 12) 
                + FechaHasta.Month - FechaDesde.Month + 1;

            int mesesTranscurridos = ((fecha.Year - FechaDesde.Year) * 12) 
                + fecha.Month - FechaDesde.Month + 1;

            int mesesRestantes = mesesTotales - mesesTranscurridos;
            return mesesRestantes * Monto;
        }
    }
}
