using CasosDeUso.DTOs.PagoDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUso.InterfacesCU.CUPago
{
    public interface ICUAltaPagoUnico
    {
        int Ejecutar(PagoUnicoDTO PagoUnicoDTO);
    }
}
