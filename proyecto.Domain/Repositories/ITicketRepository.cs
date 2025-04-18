namespace Proyecto.Domain.Repositories
{
    public interface ITicketRepository
    {
        Task<IEnumerable<object>> ObtenerTodosAsync();
        Task<object?> ObtenerPorIdAsync(int id);
        Task CrearAsync(object ticket);
        Task ActualizarAsync(object ticket);
        Task EliminarAsync(int id);
    }
}
