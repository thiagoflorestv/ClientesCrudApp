using AutoMapper;
using Clientes.Application.DTOs;
using Clientes.Application.Interfaces.Repositories;
using Clientes.Application.Interfaces.Services;
using Clientes.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Clientes.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClienteService(
            IClienteRepository repository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
                
        public async Task<IEnumerable<ClienteDto>> ObterTodosAsync()
        {
            var clientes = await _repository.ObterTodosAsync();
            return _mapper.Map<IEnumerable<ClienteDto>>(clientes);
        }
                
        public async Task<ClienteDto?> ObterPorCodigoAsync(int codigoCliente)
        {
            var cliente = await _repository.ObterPorCodigoAsync(codigoCliente);
            return cliente == null
                ? null
                : _mapper.Map<ClienteDto>(cliente);
        }
                
        public async Task<ClienteDto> AdicionarAsync(ClienteDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.RazaoSocial))
                throw new ArgumentException("Razão Social é obrigatória.");

            var cliente = _mapper.Map<Cliente>(dto);

            cliente.DataInsercao = DateTime.UtcNow;
            cliente.UsuarioInsercao = ObterUsuarioLogado();

            foreach (var tel in cliente.Telefones)
            {
                tel.DataInsercao = DateTime.UtcNow;
                tel.UsuarioInsercao = cliente.UsuarioInsercao;
                tel.Ativo = true;
            }

            await _repository.AdicionarAsync(cliente);
            return _mapper.Map<ClienteDto>(cliente);
        }

                
        public async Task<bool> AtualizarAsync(int codigoCliente, ClienteDto dto)
        {
            var cliente = await _repository.ObterPorCodigoAsync(codigoCliente);
            if (cliente == null) return false;

            _mapper.Map(dto, cliente);

            cliente.Telefones.Clear();

            foreach (var telDto in dto.Telefones)
            {
                cliente.Telefones.Add(new Telefone
                {
                    NumeroTelefone = telDto.NumeroTelefone,
                    CodigoTipoTelefone = telDto.CodigoTipoTelefone,
                    Operadora = telDto.Operadora,
                    Ativo = telDto.Ativo,
                    DataInsercao = DateTime.UtcNow,
                    UsuarioInsercao = ObterUsuarioLogado()
                });
            }

            await _repository.AtualizarAsync(cliente);
            return true;
        }
                
        public async Task<bool> RemoverAsync(int codigoCliente)
        {
            var clienteExistente = await _repository.ObterPorCodigoAsync(codigoCliente);
            if (clienteExistente == null)
                return false;

            await _repository.RemoverAsync(codigoCliente);
            return true;
        }
                
        private string ObterUsuarioLogado()
        {
            return _httpContextAccessor.HttpContext?
                       .User?
                       .FindFirst(ClaimTypes.Name)?.Value
                   ?? "Sistema";
        }
    }
}
