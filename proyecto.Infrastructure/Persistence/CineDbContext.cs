using Microsoft.EntityFrameworkCore;
// using proyecto.Domain.Entities;

namespace proyecto.Infrastructure.Persistence
{
    public class CineDbContext : DbContext
    {
        public CineDbContext(DbContextOptions<CineDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<Proyeccion> Proyecciones { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Pago> Pagos { get; set; }
    }
}