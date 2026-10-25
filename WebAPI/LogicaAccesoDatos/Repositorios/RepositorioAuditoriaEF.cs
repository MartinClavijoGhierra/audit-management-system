using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.Repositorios
{
    public class RepositorioAuditoriaEF : IRepositorioAuditoria
    {
        public EmpresaContexto Contexto { get; set; }

        public RepositorioAuditoriaEF(EmpresaContexto contexto)
        {
            Contexto = contexto;

        }

        public void Add(Auditoria item)
        {
            item.Validar();
            Contexto.Auditorias.Add(item);
            Contexto.SaveChanges();
        }


        public void Delete(Auditoria id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Auditoria> FindAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Auditoria> FindAllById(int id)
        {
            return Contexto.Auditorias
                .Include(a => a.Usuario)
                .Include(a => a.TipoGasto)
                .Where(a => a.TipoGasto.Id == id)
                .ToList();
        }

        public Auditoria FindById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Auditoria item)
        {
            throw new NotImplementedException();
        }
    }
}
