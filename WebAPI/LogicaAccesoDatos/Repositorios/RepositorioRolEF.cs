using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.Repositorios
{
    public class RepositorioRolEF :IRepositorioRol
    {
        public EmpresaContexto Contexto { get; set; }

        public RepositorioRolEF(EmpresaContexto contexto)
        {
            Contexto = contexto;
        }

        public void Add(Rol item)
        {
            throw new NotImplementedException();
        }

        public Rol FindById(int id)
        {
            return Contexto.Rols.Find(id);
            
        }

        public IEnumerable<Rol> FindAll()
        {
            return Contexto.Rols;
        }

        public void Delete(Rol item)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Rol item)
        {
            throw new NotImplementedException();
        }
    }
}
