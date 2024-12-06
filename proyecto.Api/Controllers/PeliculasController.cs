using Microsoft.AspNetCore.Mvc;
using Proyecto.Application.Services;
using Proyecto.Domain.Entities;

namespace Proyecto.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeliculasController : ControllerBase
    {
        private readonly PeliculaService _peliculaService;

        public PeliculasController(PeliculaService peliculaService)
        {
            _peliculaService = peliculaService;
        }

        [HttpGet]
        public IActionResult ObtenerPeliculas()
        {
            var peliculas = _peliculaService.ObtenerTodasLasPeliculas();
            return Ok(peliculas);
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerPelicula(int id)
        {
            var pelicula = _peliculaService.ObtenerPeliculaPorId(id);
            if (pelicula == null)
                return NotFound();
            return Ok(pelicula);
        }

        [HttpPost]
        public IActionResult AgregarPelicula([FromBody] Pelicula pelicula)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _peliculaService.AgregarPelicula(pelicula);
            return CreatedAtAction(nameof(ObtenerPelicula), new { id = pelicula.PeliculaId }, pelicula);
        }

        [HttpPut("{id}")]
        public IActionResult ActualizarPelicula(int id, [FromBody] Pelicula pelicula)
        {
            if (id != pelicula.PeliculaId)
                return BadRequest();

            _peliculaService.ActualizarPelicula(pelicula);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarPelicula(int id)
        {
            _peliculaService.EliminarPelicula(id);
            return NoContent();
        }
    }
}