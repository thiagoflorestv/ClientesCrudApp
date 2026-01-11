using Clientes.Application.Interfaces.Repositories;
using Clientes.Domain.Entities;
using Clientes.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Clientes.Infrastructure.Repositories
{
    public class TelefoneRepository : ITelefoneRepository
    {
        private readonly AppDbContext _context;

        public TelefoneRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Telefone>> ObterPorClienteAsync(int codigoCliente)
        {
            return await _context.Telefones
                .AsNoTracking()
                .Include(t => t.TipoTelefone)
                .Where(t => t.CodigoCliente == codigoCliente)
                .ToListAsync();
        }

        public async Task<Telefone?> ObterPorIdAsync(int id)
        {
            return await _context.Telefones
                .Include(t => t.TipoTelefone)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task AdicionarAsync(Telefone telefone)
        {
            telefone.DataInsercao = DateTime.UtcNow;
            await _context.Telefones.AddAsync(telefone);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Telefone telefone)
        {
            _context.Entry(telefone).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(int id)
        {
            var telefone = await _context.Telefones.FindAsync(id);
            if (telefone == null) return;

            _context.Telefones.Remove(telefone);
            await _context.SaveChangesAsync();
        }
    }
}
