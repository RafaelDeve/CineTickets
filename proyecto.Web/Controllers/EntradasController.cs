using Microsoft.AspNetCore.Mvc;
using Proyecto.Application.Services;
using Proyecto.Domain.Entities;


public class EntradasController : Controller
    {
        private readonly EntradaService _entradaService;
        private readonly ProyeccionService _proyeccionService;
        private readonly UsuarioService _usuarioService;
        public EntradasController(EntradaService entradaService, ProyeccionService proyeccionService, UsuarioService usuarioService)
        {
            _entradaService = entradaService;
            _proyeccionService = proyeccionService;
            _usuarioService = usuarioService;
        }

        public IActionResult Reporte()
        {
            var entradas = _entradaService.ObtenerTodasLasEntradas();
            if (entradas == null || !entradas.Any())
            {
                ViewData["Mensaje"] = "No se encontraron entradas.";
                entradas = Enumerable.Empty<Entrada>();
            }
            return View(entradas);
        }

        public IActionResult Comprar(int proyeccionId)
        {
            // var proyeccion = _proyeccionService.ObtenerProyeccionPorId(proyeccionId);
            // var proyeccion = _proyeccionService.ObtenerProyeccionPorIdConPelicula(proyeccionId);
            // if (proyeccion == null)
            var proyeccion = _proyeccionService.ObtenerProyeccionPorId(proyeccionId);
            if (proyeccion == null || proyeccion.Pelicula == null)
            {
                return NotFound();
            }
            return View(proyeccion);
        }

        [HttpPost]
        public IActionResult ConfirmarCompra(int proyeccionId, int usuarioId, int numeroAsiento, decimal precio)
        {
            if (proyeccionId <= 0 || usuarioId <= 0 || numeroAsiento <= 0 || precio <= 0)
            {
                ModelState.AddModelError("DatosInvalidos", "Algunos de los datos de entrada son inválidos. Por favor, verifica los valores proporcionados.");
                var proyeccion = _proyeccionService.ObtenerProyeccionPorId(proyeccionId);
                if (proyeccion == null)
                {
                    return NotFound("Proyección no encontrada");
                }
                return View("Comprar", proyeccion);
            }

            var usuario = _usuarioService.ObtenerUsuarioPorId(usuarioId);
            if (usuario == null)
            {
                return NotFound("Usuario no encontrado");
            }

            _entradaService.ComprarEntrada(proyeccionId, usuarioId, numeroAsiento, precio);
            return RedirectToAction("Confirmacion");
        }

        public IActionResult Confirmacion()
        {
            return View();
        }
    }