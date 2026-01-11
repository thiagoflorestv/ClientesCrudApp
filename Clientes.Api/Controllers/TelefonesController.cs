using Clientes.Application.DTOs;
using Clientes.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/telefones")]
public class TelefonesController : ControllerBase
{
    private readonly ITelefoneService _service;

    public TelefonesController(ITelefoneService service)
    {
        _service = service;
    }

    [HttpGet("cliente/{codigoCliente}")]
    public async Task<IActionResult> GetPorCliente(int codigoCliente)
        => Ok(await _service.ObterPorClienteAsync(codigoCliente));

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var telefone = await _service.ObterPorIdAsync(id);
        return telefone == null ? NotFound() : Ok(telefone);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] TelefoneDto dto)
    {
        var criado = await _service.AdicionarAsync(dto);
        return CreatedAtAction(nameof(Get), new { id = criado.Id }, criado);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] TelefoneDto dto)
    {
        var atualizado = await _service.AtualizarAsync(id, dto);
        return atualizado ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var removido = await _service.RemoverAsync(id);
        return removido ? NoContent() : NotFound();
    }
}
