using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.InterfacesRepositorios;
using LogicaNegocio.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.Repositorios
{
    public class RepositorioUsuarioEF: IRepositorioUsuario
    {
        public EmpresaContexto Contexto { get; set; }
        public RepositorioUsuarioEF(EmpresaContexto contexto)
        {
            Contexto = contexto;
        }


        public Usuario Login(string email, string contrasenia)
        {
            return Contexto.Usuarios
                .Include(u => u.Rol) 
                .AsEnumerable()      
                .Where(u => u.Email == email && u.Contrasenia.Valor == contrasenia)
                .SingleOrDefault();
        }




        public void Add(Usuario usuario)
        {
            if (usuario == null) throw new ArgumentNullException("usuario no encontrado");

            // Aquí genero email único verificando la DB
            usuario.Email = GenerarEmailUnico(usuario.Email);

            usuario.Validar();

            Contexto.Usuarios.Add(usuario);
            Contexto.SaveChanges();
        }

        private string GenerarEmailUnico(string emailBase)
        {
            string email = emailBase;
            Random rnd = new Random();

            while (Contexto.Usuarios.Any(u => u.Email == email))
            {
                int numero = rnd.Next(1000, 9999);
                email = emailBase.Replace("@laempresa.com", numero + "@laempresa.com");
            }

            return email;
        }

        public void Delete(Usuario item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Usuario> FindAll()
        {
            return Contexto.Usuarios;
        }


        public Usuario FindById(int id)
        {

            return Contexto.Usuarios.Find(id);



        }



        //TODO
        //Revisar como busco por email
        public Usuario FindByIdEmail(string email)
        {
            return Contexto.Usuarios
                .Where(u => u.Email == email)
                .FirstOrDefault();
          
        }
        


        public string ModificarContrasenia(string email)
        {
            Usuario usuario = FindByIdEmail(email);
            if(usuario != null)
            {
                string nuevaContrasenia = usuario.Contrasenia.GenerarNuevaContrasenia();
                usuario.Contrasenia = new Contrasenia(nuevaContrasenia);
                Contexto.SaveChanges();
                return nuevaContrasenia;
            }
            else
            {
                throw new UsuarioException("No se ha encontrado usuario con ese email. Intente nuevamente");
            }

        }




        public void Update(int id, Usuario item)
        {
            throw new NotImplementedException();
        }
    }
}
