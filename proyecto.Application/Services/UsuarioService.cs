using Proyecto.Domain.Entities;
using Proyecto.Domain.Repositories;

namespace Proyecto.Application.Services
{
    // Servicio de usuarios
    public class UsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public Usuario? ObtenerUsuarioPorId(int usuarioId)
        {
            return _usuarioRepository.ObtenerUsuarioPorId(usuarioId);
        }

        public void RegistrarUsuario(Usuario usuario)
        {
            _usuarioRepository.AgregarUsuario(usuario);
        }

        public void ActualizarUsuario(Usuario usuario)
        {
            _usuarioRepository.ActualizarUsuario(usuario);
        }

        public void EliminarUsuario(int usuarioId)
        {
            _usuarioRepository.EliminarUsuario(usuarioId);
        }
    }
}