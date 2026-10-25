using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcepcionesPropias.ExcepcionesEntidades
{
    public class ContraseniaException: Exception
    {
        public ContraseniaException()
        {
        }

        public ContraseniaException(string message) : base(message)
        {
        }

        public ContraseniaException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
