using Clientes.Application.DTOs;
using Clientes.Application.Interfaces.Repositories;
using Clientes.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Clientes.Api.Controllers
{
    [ApiController]
    [Route("api/tipostelefone")]
    public class TiposTelefoneController : ControllerBase
    {
        private readonly ITipoTelefoneRepository _repository;

        public TiposTelefoneController(ITipoTelefoneRepository repository)
        {
            _repository = repository;
        }
                
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var tipos = await _repository.ObterTodosAsync();
            return Ok(tipos);
        }
                
        [HttpGet("{codigoTipoTelefone}")]
        public async Task<IActionResult> Get(int codigoTipoTelefone)
        {
            var tipo = await _repository.ObterPorCodigoAsync(codigoTipoTelefone);
            if (tipo == null) return NotFound();
            return Ok(tipo);
        }
                
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TipoTelefone tipo)
        {
            if (tipo == null) return BadRequest();
            tipo.DataInsercao = DateTime.UtcNow;
            await _repository.AdicionarAsync(tipo);
            return CreatedAtAction(nameof(Get), new { codigoTipoTelefone = tipo.CodigoTipoTelefone }, tipo);
        }
                
        [HttpPut("{codigoTipoTelefone}")]
        public async Task<IActionResult> Put(int codigoTipoTelefone, [FromBody] TipoTelefone tipo)
        {
            var existente = await _repository.ObterPorCodigoAsync(codigoTipoTelefone);
            if (existente == null) return NotFound();

            existente.DescricaoTipoTelefone = tipo.DescricaoTipoTelefone;
            await _repository.AtualizarAsync(existente);

            return NoContent();
        }
        
        [HttpDelete("{codigoTipoTelefone}")]
        public async Task<IActionResult> Delete(int codigoTipoTelefone)
        {
            var existente = await _repository.ObterPorCodigoAsync(codigoTipoTelefone);
            if (existente == null) return NotFound();

            await _repository.RemoverAsync(codigoTipoTelefone);
            return NoContent();
        }
    }
}
