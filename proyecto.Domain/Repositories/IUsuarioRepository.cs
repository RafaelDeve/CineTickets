namespace Proyecto.Domain.Repositories
{
    using Proyecto.Domain.Entities;

     public interface IUsuarioRepository
    {
        Usuario? ObtenerUsuarioPorId(int usuarioId);
        void AgregarUsuario(Usuario usuario);
        void ActualizarUsuario(Usuario usuario);
        void EliminarUsuario(int usuarioId);
    }
}