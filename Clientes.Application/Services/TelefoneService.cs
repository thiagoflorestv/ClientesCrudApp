using AutoMapper;
using Clientes.Application.DTOs;
using Clientes.Application.Interfaces.Repositories;
using Clientes.Application.Interfaces.Services;
using Clientes.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Clientes.Application.Services
{
    public class TelefoneService : ITelefoneService
    {
        private readonly ITelefoneRepository _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TelefoneService(
            ITelefoneRepository repository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<TelefoneDto>> ObterPorClienteAsync(int codigoCliente)
        {
            var telefones = await _repository.ObterPorClienteAsync(codigoCliente);
            return _mapper.Map<IEnumerable<TelefoneDto>>(telefones);
        }

        public async Task<TelefoneDto?> ObterPorIdAsync(int id)
        {
            var telefone = await _repository.ObterPorIdAsync(id);
            return telefone == null ? null : _mapper.Map<TelefoneDto>(telefone);
        }

        public async Task<TelefoneDto> AdicionarAsync(TelefoneDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.NumeroTelefone))
                throw new ArgumentException("Número do telefone é obrigatório.");

            var entity = _mapper.Map<Telefone>(dto);
            entity.DataInsercao = DateTime.UtcNow;
            entity.UsuarioInsercao = ObterUsuarioLogado();

            await _repository.AdicionarAsync(entity);
            return _mapper.Map<TelefoneDto>(entity);
        }

        public async Task<bool> AtualizarAsync(int id, TelefoneDto dto)
        {
            var existente = await _repository.ObterPorIdAsync(id);
            if (existente == null) return false;

            _mapper.Map(dto, existente);
            await _repository.AtualizarAsync(existente);
            return true;
        }

        public async Task<bool> RemoverAsync(int id)
        {
            var existente = await _repository.ObterPorIdAsync(id);
            if (existente == null) return false;

            await _repository.RemoverAsync(id);
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
