using proyecto.Infrastructure.Persistence;
using Proyecto.Domain.Entities;
using Proyecto.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Proyecto.Infrastructure.Repositories
{
    public class PeliculaRepository : IPeliculaRepository
    {
        private readonly ApplicationDbContext _context;

        public PeliculaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Pelicula? ObtenerPeliculaPorId(int peliculaId)
        {
            return _context.Peliculas.Find(peliculaId);
        }

        public IEnumerable<Pelicula> ObtenerTodasLasPeliculas()
        {
            return _context.Peliculas.ToList();
        }

        public void AgregarPelicula(Pelicula pelicula)
        {
            _context.Peliculas.Add(pelicula);
            _context.SaveChanges();
        }

        public void ActualizarPelicula(Pelicula pelicula)
        {
            _context.Peliculas.Update(pelicula);
            _context.SaveChanges();
        }

        public void EliminarPelicula(int peliculaId)
        {
            var pelicula = _context.Peliculas.Find(peliculaId);
            if (pelicula != null)
            {
                _context.Peliculas.Remove(pelicula);
                _context.SaveChanges();
            }
        }
    }
    
}