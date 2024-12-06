using Microsoft.AspNetCore.Mvc;
using Proyecto.Application.Services;
using Proyecto.Domain.Entities;

namespace Proyecto.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProyeccionesController : ControllerBase
    {
        private readonly ProyeccionService _proyeccionService;

        public ProyeccionesController(ProyeccionService proyeccionService)
        {
            _proyeccionService = proyeccionService;
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerProyeccion(int id)
        {
            var proyeccion = _proyeccionService.ObtenerProyeccionPorId(id);
            if (proyeccion == null)
                return NotFound();
            return Ok(proyeccion);
        }

        [HttpPost]
        public IActionResult AgregarProyeccion([FromBody] Proyeccion proyeccion)
        {
            _proyeccionService.AgregarProyeccion(proyeccion);
            return CreatedAtAction(nameof(ObtenerProyeccion), new { id = proyeccion.ProyeccionId }, proyeccion);
        }

        [HttpPut("{id}")]
        public IActionResult ActualizarProyeccion(int id, [FromBody] Proyeccion proyeccion)
        {
            if (id != proyeccion.ProyeccionId)
                return BadRequest();

            _proyeccionService.ActualizarProyeccion(proyeccion);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarProyeccion(int id)
        {
            _proyeccionService.EliminarProyeccion(id);
            return NoContent();
        }
    }
}