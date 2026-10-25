using CasosDeUso.DTOs.EquipoDTOs;
using CasosDeUso.DTOs.PagoDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUso.InterfacesCU.CUEquipo
{
    public interface ICUListadoEquipo
    {
        IEnumerable<ListadoEquipoDTO> Ejecutar();
    }
}
