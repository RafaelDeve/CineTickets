using Proyecto.Domain.Entities;
using Proyecto.Domain.Repositories;

namespace Proyecto.Application.Services
{
    // Servicio de usuarios
    public class PeliculaService
    {
        private readonly IPeliculaRepository _peliculaRepository;

        public PeliculaService(IPeliculaRepository peliculaRepository)
        {
            _peliculaRepository = peliculaRepository;
        }

        public Pelicula? ObtenerPeliculaPorId(int peliculaId)
        {
            return _peliculaRepository.ObtenerPeliculaPorId(peliculaId);
        }

        // public IEnumerable<Pelicula> ObtenerTodasLasPeliculas()
        // {
        //     return _peliculaRepository.ObtenerTodasLasPeliculas();
        // }

        public IEnumerable<Pelicula> ObtenerTodasLasPeliculas()
        {
            return _peliculaRepository.ObtenerTodasLasPeliculas() ?? Enumerable.Empty<Pelicula>();
        }

        public void AgregarPelicula(Pelicula pelicula)
        {
            _peliculaRepository.AgregarPelicula(pelicula);
        }

        public void ActualizarPelicula(Pelicula pelicula)
        {
            _peliculaRepository.ActualizarPelicula(pelicula);
        }

        public void EliminarPelicula(int peliculaId)
        {
            _peliculaRepository.EliminarPelicula(peliculaId);
        }
    }
}