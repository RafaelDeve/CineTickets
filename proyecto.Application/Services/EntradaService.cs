using Proyecto.Domain.Entities;
using Proyecto.Domain.Repositories;


namespace Proyecto.Application.Services
{
    // Servicio de usuarios
    public class EntradaService
    {
        private readonly IEntradaRepository _entradaRepository;
        private readonly IProyeccionRepository _proyeccionRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public EntradaService(IEntradaRepository entradaRepository, IProyeccionRepository proyeccionRepository, IUsuarioRepository usuarioRepository)
        {
            _entradaRepository = entradaRepository;
            _proyeccionRepository = proyeccionRepository;
            _usuarioRepository = usuarioRepository;
        }

        public Entrada? ObtenerEntradaPorId(int entradaId)
        {
            return _entradaRepository.ObtenerEntradaPorId(entradaId);
        }

        public IEnumerable<Entrada> ObtenerEntradasPorUsuario(int usuarioId)
        {
            return _entradaRepository.ObtenerEntradasPorUsuario(usuarioId);
        }

        public void ComprarEntrada(int proyeccionId, int usuarioId, int numeroAsiento, decimal precio)
        {
            var proyeccion = _proyeccionRepository.ObtenerProyeccionPorId(proyeccionId);
            if (proyeccion == null)
            {
                throw new ArgumentException("Proyección no encontrada");
            }

            var usuario = _usuarioRepository.ObtenerUsuarioPorId(usuarioId);
            if (usuario == null)
            {
                throw new ArgumentException("Usuario no encontrado");
            }

            var entrada = new Entrada
            {
                Proyeccion = proyeccion,
                Usuario = usuario,
                NumeroAsiento = numeroAsiento,
                Precio = precio,
                FechaCompra = DateTime.Now
            };

            _entradaRepository.AgregarEntrada(entrada);
        }

        public void EliminarEntrada(int entradaId)
        {
            _entradaRepository.EliminarEntrada(entradaId);
        }
    }
}