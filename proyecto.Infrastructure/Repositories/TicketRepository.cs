using Proyecto.Domain.Repositories;

namespace Proyecto.Infrastructure.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        public Task<IEnumerable<object>> ObtenerTodosAsync() => Task.FromResult<IEnumerable<object>>(new List<object>());
        public Task<object?> ObtenerPorIdAsync(int id) => Task.FromResult<object?>(null);
        public Task CrearAsync(object ticket) => Task.CompletedTask;
        public Task ActualizarAsync(object ticket) => Task.CompletedTask;
        public Task EliminarAsync(int id) => Task.CompletedTask;
    }
}
