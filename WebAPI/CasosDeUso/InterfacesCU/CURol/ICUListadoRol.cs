using CasosDeUso.DTOs.RolDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUso.InterfacesCU.CURol
{
    public interface ICUListadoRol
    {
        IEnumerable<ListadoRolDTO> Ejecutar();
    }
}
