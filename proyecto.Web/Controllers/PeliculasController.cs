using Microsoft.AspNetCore.Mvc;
using Proyecto.Application.Services;
using Proyecto.Domain.Entities;

public class PeliculasController : Controller
    {
        private readonly PeliculaService _peliculaService;

        // public PeliculasController(PeliculaService peliculaService)
        // {
        //     _peliculaService = peliculaService;
        // }
        private readonly ProyeccionService _proyeccionService;

        public PeliculasController(PeliculaService peliculaService, ProyeccionService proyeccionService)
        {
            _peliculaService = peliculaService;
            _proyeccionService = proyeccionService;
        }

        // public IActionResult Index()
        // {
        //     var peliculas = _peliculaService.ObtenerTodasLasPeliculas();
        //     return View(peliculas);
        // }
        public IActionResult Index()
        {
            var peliculas = _peliculaService.ObtenerTodasLasPeliculas() ?? new List<Pelicula>();
            return View(peliculas);
        }

        public IActionResult Detalle(int id)
        {
            var pelicula = _peliculaService.ObtenerPeliculaPorId(id);
            if (pelicula == null)
            {
                return NotFound();
            }
            return View(pelicula);
        }

         public IActionResult Crear()
        {
            var pelicula = new Pelicula
            {
                Titulo = string.Empty,
                Descripcion = string.Empty,
                Genero = string.Empty,
                Duracion = TimeSpan.Zero,
                Proyecciones = new List<Proyeccion>()
            };
            return View(pelicula);
        }

        [HttpPost]
public IActionResult Crear(Pelicula pelicula)
{
    if (ModelState.IsValid)
    {
        if (string.IsNullOrWhiteSpace(pelicula.Titulo) || string.IsNullOrWhiteSpace(pelicula.Descripcion) || string.IsNullOrWhiteSpace(pelicula.Genero) || pelicula.Duracion == TimeSpan.Zero)
        {
            ModelState.AddModelError("DatosInvalidos", "Todos los campos requeridos deben tener valores válidos.");
            return View(pelicula);
        }
        try
        {
            pelicula.Proyecciones = new List<Proyeccion>(); // Inicializamos Proyecciones como una lista vacía para evitar el warning
            _peliculaService.AgregarPelicula(pelicula);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("Error", "Ocurrió un error al agregar la película: " + ex.Message);
        }
    }
    return View(pelicula);
}

        public IActionResult Editar(int id)
        {
            var pelicula = _peliculaService.ObtenerPeliculaPorId(id);
            if (pelicula == null)
            {
                return NotFound();
            }
            return View(pelicula);
        }

        [HttpPost]
        public IActionResult Editar(Pelicula pelicula)
        {
            if (ModelState.IsValid)
            {
                _peliculaService.ActualizarPelicula(pelicula);
                return RedirectToAction("Index");
            }
            return View(pelicula);
        }

        public IActionResult Eliminar(int id)
        {
            var pelicula = _peliculaService.ObtenerPeliculaPorId(id);
            if (pelicula == null)
            {
                return NotFound();
            }
            return View(pelicula);
        }

        [HttpPost, ActionName("Eliminar")]
        public IActionResult EliminarConfirmado(int id)
        {
            _peliculaService.EliminarPelicula(id);
            return RedirectToAction("Index");
        }

        public IActionResult Proyecciones(int peliculaId)
        {
            // var proyecciones = _peliculaService.ObtenerProyeccionesPorPelicula(peliculaId);
            var proyecciones = _proyeccionService.ObtenerProyeccionesPorPelicula(peliculaId);
            if (proyecciones == null || !proyecciones.Any())
            {
                ViewData["Mensaje"] = "No se encontraron proyecciones para esta película.";
                proyecciones = Enumerable.Empty<Proyeccion>();
            }
            return View(proyecciones);
        }
    }