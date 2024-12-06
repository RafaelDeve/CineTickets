using Proyecto.Domain.Entities;
using Proyecto.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using proyecto.Infrastructure.Persistence;

namespace Proyecto.Application.Services
{
    // Servicio de usuarios
    public class ProyeccionService
    {
        private readonly IProyeccionRepository _proyeccionRepository;

        public ProyeccionService(IProyeccionRepository proyeccionRepository)
        {
            _proyeccionRepository = proyeccionRepository;
        }

        // public Proyeccion ObtenerProyeccionPorId(int proyeccionId)
        // {
        //     return _proyeccionRepository.ObtenerProyeccionPorId(proyeccionId);
        // }
        private readonly ApplicationDbContext? _context;

        public ProyeccionService(ApplicationDbContext context, IProyeccionRepository proyeccionRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _proyeccionRepository = proyeccionRepository ?? throw new ArgumentNullException(nameof(proyeccionRepository));
        }
        public Proyeccion ObtenerProyeccionPorId(int proyeccionId)
        {
            var proyeccion = _context?.Proyecciones
                                    .Include(p => p.Pelicula)
                                    .FirstOrDefault(p => p.ProyeccionId == proyeccionId);

            if (proyeccion == null)
            {
                throw new InvalidOperationException($"No se encontró una proyección con ID {proyeccionId}");
            }

            return proyeccion;
        }


        public IEnumerable<Proyeccion> ObtenerProyeccionesPorPelicula(int peliculaId)
        {
            return _proyeccionRepository.ObtenerProyeccionesPorPelicula(peliculaId);
        }

        public void AgregarProyeccion(Proyeccion proyeccion)
        {
            _proyeccionRepository.AgregarProyeccion(proyeccion);
        }

        public void ActualizarProyeccion(Proyeccion proyeccion)
        {
            _proyeccionRepository.ActualizarProyeccion(proyeccion);
        }

        public void EliminarProyeccion(int proyeccionId)
        {
            _proyeccionRepository.EliminarProyeccion(proyeccionId);
        }
    }
}