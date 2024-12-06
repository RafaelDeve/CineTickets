using proyecto.Infrastructure.Persistence;
using Proyecto.Domain.Entities;
using Proyecto.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Proyecto.Infrastructure.Repositories
{
    public class ProyeccionRepository : IProyeccionRepository
    {
        private readonly ApplicationDbContext _context;

        public ProyeccionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Proyeccion? ObtenerProyeccionPorId(int proyeccionId)
        {
            return _context.Proyecciones.Find(proyeccionId);
        }

        public IEnumerable<Proyeccion> ObtenerProyeccionesPorPelicula(int peliculaId)
        {
            return _context.Proyecciones.Where(p => p.PeliculaId == peliculaId).ToList();
        }

        public void AgregarProyeccion(Proyeccion proyeccion)
        {
            _context.Proyecciones.Add(proyeccion);
            _context.SaveChanges();
        }

        public void ActualizarProyeccion(Proyeccion proyeccion)
        {
            _context.Proyecciones.Update(proyeccion);
            _context.SaveChanges();
        }

        public void EliminarProyeccion(int proyeccionId)
        {
            var proyeccion = _context.Proyecciones.Find(proyeccionId);
            if (proyeccion != null)
            {
                _context.Proyecciones.Remove(proyeccion);
                _context.SaveChanges();
            }
        }
    } 
}