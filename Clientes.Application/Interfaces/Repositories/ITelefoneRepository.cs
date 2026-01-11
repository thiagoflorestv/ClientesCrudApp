using Clientes.Domain.Entities;

namespace Clientes.Application.Interfaces.Repositories
{
    public interface ITelefoneRepository
    {
        Task<IEnumerable<Telefone>> ObterPorClienteAsync(int codigoCliente);
        Task<Telefone?> ObterPorIdAsync(int id);
        Task AdicionarAsync(Telefone telefone);
        Task AtualizarAsync(Telefone telefone);
        Task RemoverAsync(int id);
    }
}
