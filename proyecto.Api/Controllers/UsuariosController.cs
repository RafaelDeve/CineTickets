using Microsoft.AspNetCore.Mvc;
using Proyecto.Application.Services;
using Proyecto.Domain.Entities;

namespace Proyecto.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public UsuariosController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerUsuario(int id)
        {
            var usuario = _usuarioService.ObtenerUsuarioPorId(id);
            if (usuario == null)
                return NotFound();
            return Ok(usuario);
        }

        [HttpPost]
        public IActionResult RegistrarUsuario([FromBody] Usuario usuario)
        {
            _usuarioService.RegistrarUsuario(usuario);
            return CreatedAtAction(nameof(ObtenerUsuario), new { id = usuario.UsuarioId }, usuario);
        }

        [HttpPut("{id}")]
        public IActionResult ActualizarUsuario(int id, [FromBody] Usuario usuario)
        {
            if (id != usuario.UsuarioId)
                return BadRequest();

            _usuarioService.ActualizarUsuario(usuario);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarUsuario(int id)
        {
            _usuarioService.EliminarUsuario(id);
            return NoContent();
        }
    }
}