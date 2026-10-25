using Azure;
using LogicaNegocio.EntidadesNegocio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos
{
    public class EmpresaContexto : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Equipo> Equipos { get; set; }

        public DbSet<Pago> Pagos { get; set; }

        public DbSet<Rol> Rols { get; set; }
        public DbSet<Auditoria> Auditorias { get; set; }

        public DbSet<PagoUnico> PagosUnicos { get; set; }

        public DbSet<PagoRecurrente> PagosRecurrentes { get; set; }

        public DbSet<TipoGasto> TiposGastos { get; set; }

        public EmpresaContexto(DbContextOptions options) : base(options) 
        {     
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pago>().HasOne(p => p.TipoGasto).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Pago>().HasOne(p => p.Usuario).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Usuario>().HasOne(u => u.Equipo).WithMany().OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PagoUnico>().Property(p => p.NumeroRecibo).IsRequired();


            modelBuilder.Entity<Usuario>().HasOne(u => u.Equipo).WithMany(e => e.UsuariosEquipo).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Auditoria>().HasOne(a => a.TipoGasto).WithMany().OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Auditoria>().HasOne(a => a.Usuario).WithMany().OnDelete(DeleteBehavior.SetNull);


            modelBuilder.Entity<Equipo>().HasIndex(e => e.NombreEquipo).IsUnique();
            modelBuilder.Entity<TipoGasto>().HasIndex(t => t.Nombre).IsUnique();
            modelBuilder.Entity<Usuario>().HasIndex(u => u.Email).IsUnique();
            base.OnModelCreating(modelBuilder);





        }
    }
}
