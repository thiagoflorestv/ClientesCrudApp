using Clientes.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clientes.Application.Interfaces.Repositories
{
    public interface ITipoTelefoneRepository
    {   
        Task<IEnumerable<TipoTelefone>> ObterTodosAsync();

        Task<TipoTelefone?> ObterPorCodigoAsync(int codigoTipoTelefone);

        Task AdicionarAsync(TipoTelefone tipoTelefone);

        Task AtualizarAsync(TipoTelefone tipoTelefone);
                
        Task RemoverAsync(int codigoTipoTelefone);
    }
}
