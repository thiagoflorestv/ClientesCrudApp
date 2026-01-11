using Clientes.Application.Interfaces.Repositories;
using Clientes.Domain.Entities;
using Clientes.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Clientes.Infrastructure.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbContext _context;

        public ClienteRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // Lista todos os clientes
        public async Task<IEnumerable<Cliente>> ObterTodosAsync()
        {
            return await _context.Clientes
                .AsNoTracking()
                .Include(c => c.Telefones)
                    .ThenInclude(t => t.TipoTelefone)
                .ToListAsync();
        }

        // Obtém cliente por código
        public async Task<Cliente?> ObterPorCodigoAsync(int codigoCliente)
        {
            return await _context.Clientes
                .Include(c => c.Telefones)
                    .ThenInclude(t => t.TipoTelefone)
                .FirstOrDefaultAsync(c => c.CodigoCliente == codigoCliente);
        }

        // Adiciona cliente
        public async Task AdicionarAsync(Cliente cliente)
        {
            ArgumentNullException.ThrowIfNull(cliente);

            cliente.DataInsercao = DateTime.UtcNow;

            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();
        }

        // Atualiza cliente
        public async Task AtualizarAsync(Cliente cliente)
        {
            ArgumentNullException.ThrowIfNull(cliente);

            _context.Entry(cliente).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        // Remove cliente
        public async Task RemoverAsync(int codigoCliente)
        {
            var cliente = await _context.Clientes
                .Include(c => c.Telefones)
                .FirstOrDefaultAsync(c => c.CodigoCliente == codigoCliente);

            if (cliente == null)
                return;

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
        }
    }
}
