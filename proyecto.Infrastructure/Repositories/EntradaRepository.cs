using proyecto.Infrastructure.Persistence;
using Proyecto.Domain.Entities;
using Proyecto.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Proyecto.Infrastructure.Repositories
{
    public class EntradaRepository : IEntradaRepository
    {
        private readonly ApplicationDbContext _context;

        public EntradaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Entrada? ObtenerEntradaPorId(int entradaId)
        {
            return _context.Entradas.Find(entradaId);
        }

        public IEnumerable<Entrada> ObtenerEntradasPorUsuario(int usuarioId)
        {
            return _context.Entradas.Where(e => e.UsuarioId == usuarioId).ToList();
        }

        public void AgregarEntrada(Entrada entrada)
        {
            _context.Entradas.Add(entrada);
            _context.SaveChanges();
        }

        public void ActualizarEntrada(Entrada entrada)
        {
            _context.Entradas.Update(entrada);
            _context.SaveChanges();
        }

        public void EliminarEntrada(int entradaId)
        {
            var entrada = _context.Entradas.Find(entradaId);
            if (entrada != null)
            {
                _context.Entradas.Remove(entrada);
                _context.SaveChanges();
            }
        }
    }
}