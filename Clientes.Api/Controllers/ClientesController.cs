using Clientes.Application.DTOs;
using Clientes.Application.Interfaces.Services;
using Clientes.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Clientes.Api.Controllers
{
    [ApiController]
    [Route("api/clientes")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _service;


        public ClientesController(IClienteService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var clientes = await _service.ObterTodosAsync();
            return Ok(clientes);
        }

        [HttpGet("BuscarClientePorId/{codigoCliente}")]
        public async Task<IActionResult> Get(int codigoCliente)
        {
            var cliente = await _service.ObterPorCodigoAsync(codigoCliente);
            if (cliente == null) return NotFound();
            return Ok(cliente);
        }

        [HttpPost("SalvarCliente")]
        public async Task<IActionResult> Post([FromBody] ClienteDto dto)
        {
            var clienteCriado = await _service.AdicionarAsync(dto);
            return CreatedAtAction(nameof(Get), new { codigoCliente = clienteCriado.CodigoCliente }, clienteCriado);
        }

        [HttpPut("AtualizarClientePorId/{codigoCliente}")]
        public async Task<IActionResult> Put(int codigoCliente, [FromBody] ClienteDto dto)
        {
            var atualizado = await _service.AtualizarAsync(codigoCliente, dto);
            if (!atualizado) return NotFound();
            return NoContent();
        }

        [HttpDelete("ExcluirClientePorId/{codigoCliente}")]
        public async Task<IActionResult> Delete(int codigoCliente)
        {
            var removido = await _service.RemoverAsync(codigoCliente);
            if (!removido) return NotFound();
            return NoContent();
        }
    }
}
