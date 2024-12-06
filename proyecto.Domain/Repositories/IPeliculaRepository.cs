namespace Proyecto.Domain.Repositories
{
    using Proyecto.Domain.Entities;

    public interface IPeliculaRepository
    {
        public Pelicula? ObtenerPeliculaPorId(int peliculaId);
        IEnumerable<Pelicula> ObtenerTodasLasPeliculas();
        void AgregarPelicula(Pelicula pelicula);
        void ActualizarPelicula(Pelicula pelicula);
        void EliminarPelicula(int peliculaId);
    }
}