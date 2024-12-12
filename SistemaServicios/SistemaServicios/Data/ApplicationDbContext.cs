using Microsoft.EntityFrameworkCore;
using SistemaServicios.Models;

namespace SistemaServicios.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Tecnico> Tecnicos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<CategoriaServicio> CategoriasServicio { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<SolicitudServicio> SolicitudesServicio { get; set; }
        public DbSet<Horario> Horarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Asegúrate de que CategoriaId sea la clave primaria
            modelBuilder.Entity<CategoriaServicio>()
                        .HasKey(c => c.CategoriaId);  // Define explícitamente la clave primaria

            // Asegúrate de que SolicitudId sea la clave primaria
            modelBuilder.Entity<SolicitudServicio>()
                        .HasKey(s => s.SolicitudId);  // Define explícitamente la clave primaria

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.NombreUsuario)
                .IsUnique();

            modelBuilder.Entity<Cliente>()
                .HasIndex(c => c.Documento)
                .IsUnique();
        }
    }
}
