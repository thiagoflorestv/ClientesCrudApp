using Clientes.Application.DTOs;

namespace Clientes.Application.Interfaces.Services
{
    public interface ITelefoneService
    {
        Task<IEnumerable<TelefoneDto>> ObterPorClienteAsync(int codigoCliente);
        Task<TelefoneDto?> ObterPorIdAsync(int id);
        Task<TelefoneDto> AdicionarAsync(TelefoneDto dto);
        Task<bool> AtualizarAsync(int id, TelefoneDto dto);
        Task<bool> RemoverAsync(int id);
    }
}
