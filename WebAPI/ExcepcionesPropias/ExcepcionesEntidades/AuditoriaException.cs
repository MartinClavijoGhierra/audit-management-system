using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcepcionesPropias.ExcepcionesEntidades
{
    public class AuditoriaException : Exception
    {
        public AuditoriaException()
        {
        }

        public AuditoriaException(string message) : base(message)
        {
        }

        public AuditoriaException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
