using CasosDeUso.DTOs.AuditoriaDTOs;
using CasosDeUso.DTOs.TipoGastosDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUso.InterfacesCU.CUAuditoria
{
    public interface ICUAltaAuditoria
    {
        void Ejecutar(AuditoriaDTO auditoriaDTO);
    }
}
