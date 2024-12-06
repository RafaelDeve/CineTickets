using Microsoft.EntityFrameworkCore;
using Proyecto.Domain.Entities;

namespace proyecto.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<Proyeccion> Proyecciones { get; set; }
        public DbSet<Entrada> Entradas { get; set; }
        public DbSet<Pago> Pagos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configuraciones adicionales si es necesario
        }
    }
}