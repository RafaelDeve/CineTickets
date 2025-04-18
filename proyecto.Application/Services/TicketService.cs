using Proyecto.Domain.Repositories;

namespace Proyecto.Application.Services
{
    public class TicketService
    {
        private readonly ITicketRepository _repository;

        public TicketService(ITicketRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<object>> ObtenerTodosAsync() => _repository.ObtenerTodosAsync();
    }
}
