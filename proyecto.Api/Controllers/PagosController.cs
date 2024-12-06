using Microsoft.AspNetCore.Mvc;
using Proyecto.Application.Services;
using Proyecto.Domain.Entities;

namespace Proyecto.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagosController : ControllerBase
    {
        private readonly PagoService _pagoService;

        public PagosController(PagoService pagoService)
        {
            _pagoService = pagoService;
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerPago(int id)
        {
            var pago = _pagoService.ObtenerPagoPorId(id);
            if (pago == null)
                return NotFound();
            return Ok(pago);
        }

        [HttpPost]
        public IActionResult RealizarPago([FromBody] Pago pago)
        {
            _pagoService.RealizarPago(pago.EntradaId, pago.Monto, pago.MetodoPago);
            return CreatedAtAction(nameof(ObtenerPago), new { id = pago.PagoId }, pago);
        }
    }
}