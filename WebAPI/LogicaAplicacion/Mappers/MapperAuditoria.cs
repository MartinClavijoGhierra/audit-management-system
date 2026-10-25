using CasosDeUso.DTOs.AuditoriaDTOs;
using CasosDeUso.DTOs.TipoGastosDTOs;
using LogicaNegocio.EntidadesNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Mappers
{
    public class MapperAuditoria
    {
        public static Auditoria DTOAuditoriaToAuditoria(AuditoriaDTO auditoriaDTO, Usuario usuario, TipoGasto gasto)
        {
            if (auditoriaDTO == null)
            {
                throw new Exception("Los datos no son correctos");
            }
            
            return new Auditoria (usuario,auditoriaDTO.Accion, gasto, gasto.Activo);

        }

        internal static IEnumerable<ListadoAuditoriaDTO> auditoriaToListadoAuditoriaDTO(IEnumerable<Auditoria> auditorias)
        {
            if (auditorias == null)
            {
                throw new Exception("Los datos no son correctos");
            }
            else
            {
                List<ListadoAuditoriaDTO> auditoriasDTO = new List<ListadoAuditoriaDTO>();
                foreach(Auditoria auditoria in auditorias)
                {
                    auditoriasDTO.Add(new ListadoAuditoriaDTO()
                    {
                        Fecha = auditoria.Fecha,
                        Accion = auditoria.Accion,
                        TipoGasto = auditoria.TipoGasto.Nombre,
                        UsuarioId = auditoria.Usuario.Id,
                        UsuarioName = auditoria.Usuario.Nombre,
                        Activo = auditoria.Activo,

                    });
                }
                return auditoriasDTO;
            }
        }
    }
}
