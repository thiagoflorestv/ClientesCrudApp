using Clientes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clientes.Application.Interfaces.Repositories
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> ObterTodosAsync();
        Task<Cliente?> ObterPorCodigoAsync(int codigoCliente);
        Task AdicionarAsync(Cliente cliente);
        Task AtualizarAsync(Cliente cliente);
        Task RemoverAsync(int codigoCliente);
    }

}
