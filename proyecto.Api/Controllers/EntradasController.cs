using Microsoft.AspNetCore.Mvc;
using Proyecto.Application.Services;
using Proyecto.Domain.Entities;

namespace Proyecto.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EntradasController : ControllerBase
    {
        private readonly EntradaService _entradaService;

        public EntradasController(EntradaService entradaService)
        {
            _entradaService = entradaService;
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerEntrada(int id)
        {
            var entrada = _entradaService.ObtenerEntradaPorId(id);
            if (entrada == null)
                return NotFound();
            return Ok(entrada);
        }

        [HttpPost]
        public IActionResult ComprarEntrada([FromBody] Entrada entrada)
        {
            _entradaService.ComprarEntrada(entrada.ProyeccionId, entrada.UsuarioId, entrada.NumeroAsiento, entrada.Precio);
            return CreatedAtAction(nameof(ObtenerEntrada), new { id = entrada.EntradaId }, entrada);
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarEntrada(int id)
        {
            _entradaService.EliminarEntrada(id);
            return NoContent();
        }
    }
}