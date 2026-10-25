using CasosDeUso.DTOs.TipoGastosDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUso.InterfacesCU.CUTipoGastos
{
    public interface ICUModificarGasto
    {
        void Ejecutar(int id, ListadoTipoGastoDTO gasto);
    }
}
