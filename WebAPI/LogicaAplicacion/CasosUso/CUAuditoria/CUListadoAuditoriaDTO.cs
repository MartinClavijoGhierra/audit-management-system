using CasosDeUso.DTOs.AuditoriaDTOs;
using CasosDeUso.InterfacesCU.CUAuditoria;
using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaAplicacion.Mappers;
using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUAuditoria
{
    public class CUListadoAuditoriaDTO : ICUListadoAuditoriaDTO
    {
        public IRepositorioAuditoria RepoAuditoria { get; set; }
        public CUListadoAuditoriaDTO(IRepositorioAuditoria repoAuditoria)
        {
            RepoAuditoria = repoAuditoria;
        }
        public IEnumerable<ListadoAuditoriaDTO> Ejecutar(int id)
        {
            if(id <= 0)
            {
                throw new AuditoriaException("ID ingresado no valido");
            }
            IEnumerable<Auditoria> auditorias = RepoAuditoria.FindAllById(id);
            return MapperAuditoria.auditoriaToListadoAuditoriaDTO(auditorias);
        }
    }
}
