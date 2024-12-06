using proyecto.Infrastructure.Persistence;
using Proyecto.Domain.Entities;
using Proyecto.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Proyecto.Infrastructure.Repositories
{
    public class PagoRepository : IPagoRepository
    {
        private readonly ApplicationDbContext _context;

        public PagoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Pago? ObtenerPagoPorId(int pagoId)
        {
            return _context.Pagos.Find(pagoId);
        }

        public void AgregarPago(Pago pago)
        {
            _context.Pagos.Add(pago);
            _context.SaveChanges();
        }
    }
    
}