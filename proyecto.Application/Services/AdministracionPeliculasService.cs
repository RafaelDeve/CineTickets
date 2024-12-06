using Proyecto.Domain.Entities;
using Proyecto.Domain.Repositories;


namespace Proyecto.Application.Services
{
    // Servicio de usuarios
    public class AdministracionPeliculasService
    {
        private readonly IPeliculaRepository _peliculaRepository;

        public AdministracionPeliculasService(IPeliculaRepository peliculaRepository)
        {
            _peliculaRepository = peliculaRepository;
        }

        public IEnumerable<Pelicula> ObtenerTodasLasPeliculas()
        {
            return _peliculaRepository.ObtenerTodasLasPeliculas();
        }

        public Pelicula? ObtenerPeliculaPorId(int peliculaId)
        {
            return _peliculaRepository.ObtenerPeliculaPorId(peliculaId);
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