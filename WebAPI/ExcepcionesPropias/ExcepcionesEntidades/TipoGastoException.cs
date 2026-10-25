using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcepcionesPropias.ExcepcionesEntidades
{
    public class TipoGastoException:Exception
    {
        public TipoGastoException()
        {
        }

        public TipoGastoException(string message) : base(message)
        {
        }

        public TipoGastoException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
