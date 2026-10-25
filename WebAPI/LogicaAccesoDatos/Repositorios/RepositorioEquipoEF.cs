using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.Repositorios
{
    public class RepositorioEquipoEF : IRepositorioEquipo
    {
        public EmpresaContexto Contexto { get; set; }
        public RepositorioEquipoEF(EmpresaContexto contexto)
        {
            Contexto = contexto;
        }

        public void Add(Equipo item)
        {
            throw new NotImplementedException();
        }

        public void Delete(Equipo item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Equipo> FindAll()
        {
            return Contexto.Equipos;
        }

        public Equipo FindById(int id)
        {
            return Contexto.Equipos.Find(id);
        }

        public void Update(int id, Equipo item)
        {
            throw new NotImplementedException();
        }
    }
}
