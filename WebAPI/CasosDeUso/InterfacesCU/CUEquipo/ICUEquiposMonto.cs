using CasosDeUso.DTOs.EquipoDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUso.InterfacesCU.CUEquipo
{
    public interface ICUEquiposMonto
    {
        IEnumerable<ListadoEquipoDTO> Ejecutar(decimal monto);
    }
}
