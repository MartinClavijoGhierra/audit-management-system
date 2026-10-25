using CasosDeUso.DTOs.AuditoriaDTOs;
using CasosDeUso.DTOs.TipoGastosDTOs;
using CasosDeUso.InterfacesCU.CUAuditoria;
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
    public class CUAltaAuditoria : ICUAltaAuditoria
    {
        public IRepositorioAuditoria RepoAuditoria { get; set; }
        public IRepositorioUsuario RepoUsuario { get; set; }
        public IRepositorioTipoGasto RepoGasto { get; set; }
        
        public CUAltaAuditoria(IRepositorioAuditoria repoAuditoria, 
            IRepositorioUsuario repoUsuario, 
            IRepositorioTipoGasto repoGasto)
        {
            RepoAuditoria = repoAuditoria;
            RepoUsuario = repoUsuario;
            RepoGasto = repoGasto;
            
        }


        public void Ejecutar(AuditoriaDTO auditoriaDTO)
        {
            Usuario usuario = RepoUsuario.FindById(auditoriaDTO.UsuarioId);
            TipoGasto gasto = RepoGasto.FindById(auditoriaDTO.TipoGasto);

            Auditoria auditoria = MapperAuditoria.DTOAuditoriaToAuditoria(auditoriaDTO, usuario, gasto);
            RepoAuditoria.Add(auditoria);
        }

   
    }
}
