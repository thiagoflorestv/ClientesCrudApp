using Clientes.Application.Interfaces.Repositories;
using Clientes.Domain.Entities;
using Clientes.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clientes.Infrastructure.Repositories
{
    public class TipoTelefoneRepository : ITipoTelefoneRepository
    {
        private readonly AppDbContext _context;

        public TipoTelefoneRepository(AppDbContext context)
        {
            _context = context;
        }

        // Listar todos
        public async Task<IEnumerable<TipoTelefone>> ObterTodosAsync()
        {
            return await _context.TiposTelefone!.AsNoTracking().ToListAsync();
        }

        // Obter por código
        public async Task<TipoTelefone?> ObterPorCodigoAsync(int codigoTipoTelefone)
        {
            return await _context.TiposTelefone!
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.CodigoTipoTelefone == codigoTipoTelefone);
        }

        // Adicionar novo tipo de telefone
        public async Task AdicionarAsync(TipoTelefone tipoTelefone)
        {
            tipoTelefone.DataInsercao = System.DateTime.UtcNow;
            await _context.TiposTelefone!.AddAsync(tipoTelefone);
            await _context.SaveChangesAsync();
        }

        // Atualizar tipo de telefone existente
        public async Task AtualizarAsync(TipoTelefone tipoTelefone)
        {
            _context.TiposTelefone!.Update(tipoTelefone);
            await _context.SaveChangesAsync();
        }

        // Remover por código
        public async Task RemoverAsync(int codigoTipoTelefone)
        {
            var tipoTelefone = await _context.TiposTelefone!
                .FirstOrDefaultAsync(t => t.CodigoTipoTelefone == codigoTipoTelefone);

            if (tipoTelefone != null)
            {
                _context.TiposTelefone.Remove(tipoTelefone);
                await _context.SaveChangesAsync();
            }
        }
    }
}
