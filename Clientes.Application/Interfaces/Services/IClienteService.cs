using Clientes.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clientes.Application.Interfaces.Services
{
    public interface IClienteService
    {
        Task<IEnumerable<ClienteDto>> ObterTodosAsync();
        Task<ClienteDto?> ObterPorCodigoAsync(int codigoCliente);
        Task<ClienteDto> AdicionarAsync(ClienteDto dto);
        Task<bool> AtualizarAsync(int codigoCliente, ClienteDto dto);
        Task<bool> RemoverAsync(int codigoCliente);
    }
}
