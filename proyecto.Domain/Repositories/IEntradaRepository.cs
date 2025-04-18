namespace Proyecto.Domain.Repositories
{
    using Proyecto.Domain.Entities;
    public interface IEntradaRepository
    {
        
        Entrada? ObtenerEntradaPorId(int entradaId);
        IEnumerable<Entrada> ObtenerTodas();
        IEnumerable<Entrada> ObtenerEntradasPorUsuario(int usuarioId);
        void AgregarEntrada(Entrada entrada);
        void ActualizarEntrada(Entrada entrada);
        void EliminarEntrada(int entradaId);
    }
}


