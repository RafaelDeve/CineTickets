using Proyecto.Domain.Entities;
using Proyecto.Domain.Repositories;


namespace Proyecto.Application.Services
{
    // Servicio de usuarios
    public class PagoService
    {
        private readonly IPagoRepository _pagoRepository;
        private readonly IEntradaRepository _entradaRepository;

        public PagoService(IPagoRepository pagoRepository, IEntradaRepository entradaRepository)
        {
            _pagoRepository = pagoRepository;
            _entradaRepository = entradaRepository;
        }

        public Pago? ObtenerPagoPorId(int pagoId)
        {
            return _pagoRepository.ObtenerPagoPorId(pagoId);
        }

        public void RealizarPago(int entradaId, decimal monto, string metodoPago)
        {
            var entrada = _entradaRepository.ObtenerEntradaPorId(entradaId);
            if (entrada == null)
            {
                throw new ArgumentException("Entrada no encontrada");
            }

            var pago = new Pago
            {
                Entrada = entrada,
                Monto = monto,
                MetodoPago = metodoPago,
                FechaPago = DateTime.Now
            };

            _pagoRepository.AgregarPago(pago);
        }
    }
}