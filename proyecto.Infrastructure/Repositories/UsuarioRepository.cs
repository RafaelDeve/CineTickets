using proyecto.Infrastructure.Persistence;
using Proyecto.Domain.Entities;
using Proyecto.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Proyecto.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Usuario? ObtenerUsuarioPorId(int usuarioId)
        {
            return _context.Usuarios.Find(usuarioId);
        }

        public void AgregarUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        public void ActualizarUsuario(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            _context.SaveChanges();
        }

        public void EliminarUsuario(int usuarioId)
        {
            var usuario = _context.Usuarios.Find(usuarioId);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                _context.SaveChanges();
            }
        }
    }
}