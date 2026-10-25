using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaNegocio.InterfacesEntidades;
using LogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.EntidadesNegocio
{
    public class PagoUnico : Pago, IValidable
    {
        public DateTime FechaPago { get; set; }
        public int NumeroRecibo { get; set; }


        public PagoUnico(): base()
        {
            
        }

        public PagoUnico(DateTime fechaPago, int numeroRecibo, 
            MetodoPago metodoPago, Usuario usuario, TipoGasto tipoGasto, string description, decimal monto) 
            : base(metodoPago, usuario, tipoGasto, description, monto)
        {
            FechaPago = fechaPago;
            NumeroRecibo = numeroRecibo;
            Validar();
        }

        public void Validar()
        {
            ValidarRecibo();
        }


        private void ValidarRecibo()
        {
            if (NumeroRecibo <=0 || NumeroRecibo == null)
            {
                throw new PagoException("el numero de recibo no puede ser menor a 1");
            }
        }

        public override decimal CalcularMontoTotal()
        {
            return base.Monto;
        }

        public override decimal CalcularMontoPendiente(DateTime fecha)
        {
            return 0;
        }
    }
}
