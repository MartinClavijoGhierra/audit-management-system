using CasosDeUso.DTOs.AuditoriaDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUso.InterfacesCU.CUAuditoria
{
    public interface ICUListadoAuditoriaDTO
    {
        IEnumerable<ListadoAuditoriaDTO> Ejecutar(int id);
    }
}
