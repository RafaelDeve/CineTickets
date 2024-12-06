namespace Proyecto.Domain.Repositories
{
    using Proyecto.Domain.Entities;

    public interface IProyeccionRepository
    {
        Proyeccion? ObtenerProyeccionPorId(int proyeccionId);
        IEnumerable<Proyeccion> ObtenerProyeccionesPorPelicula(int peliculaId);
        void AgregarProyeccion(Proyeccion proyeccion);
        void ActualizarProyeccion(Proyeccion proyeccion);
        void EliminarProyeccion(int proyeccionId);
    }
}
