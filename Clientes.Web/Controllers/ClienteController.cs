using Clientes.Application.DTOs;
using Clientes.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Clientes.Web.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }
                
        public async Task<IActionResult> Index()
        {
            var clientes = await _clienteService.ObterTodosAsync();
            return View(clientes);
        }

        
        public IActionResult Create()
        {
            var model = new ClienteDto
            {
                Telefones = new List<TelefoneDto>()
            };

            ViewBag.TiposTelefone = new List<object>
            {
                new { CodigoTipoTelefone = 1, DescricaoTipoTelefone = "Celular" },
                new { CodigoTipoTelefone = 2, DescricaoTipoTelefone = "Residencial" },
                new { CodigoTipoTelefone = 3, DescricaoTipoTelefone = "Comercial" }
            };

            return View(model);
        }
                
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClienteDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.TiposTelefone = new List<object>
                {
                    new { CodigoTipoTelefone = 1, DescricaoTipoTelefone = "Celular" },
                    new { CodigoTipoTelefone = 2, DescricaoTipoTelefone = "Residencial" },
                    new { CodigoTipoTelefone = 3, DescricaoTipoTelefone = "Comercial" }
                };
                return View(dto);
            }

            await _clienteService.AdicionarAsync(dto);
            return RedirectToAction(nameof(Index));
        }
                
        public async Task<IActionResult> Edit(int id)
        {
            var cliente = await _clienteService.ObterPorCodigoAsync(id);
            if (cliente == null) return NotFound();

            ViewBag.TiposTelefone = new List<object>
            {
                new { CodigoTipoTelefone = 1, DescricaoTipoTelefone = "Celular" },
                new { CodigoTipoTelefone = 2, DescricaoTipoTelefone = "Residencial" },
                new { CodigoTipoTelefone = 3, DescricaoTipoTelefone = "Comercial" }
            };

            return View(cliente);
        }
                
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ClienteDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.TiposTelefone = new List<object>
                {
                    new { CodigoTipoTelefone = 1, DescricaoTipoTelefone = "Celular" },
                    new { CodigoTipoTelefone = 2, DescricaoTipoTelefone = "Residencial" },
                    new { CodigoTipoTelefone = 3, DescricaoTipoTelefone = "Comercial" }
                };
                return View(dto);
            }

            var atualizado = await _clienteService.AtualizarAsync(id, dto);
            if (!atualizado) return NotFound();

            return RedirectToAction(nameof(Index));
        }
                
        public async Task<IActionResult> Details(int id)
        {
            var cliente = await _clienteService.ObterPorCodigoAsync(id);
            if (cliente == null) return NotFound();
            return View(cliente);
        }
                
        public async Task<IActionResult> Delete(int id)
        {
            var cliente = await _clienteService.ObterPorCodigoAsync(id);
            if (cliente == null) return NotFound();
            return View(cliente);
        }
                
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sucesso = await _clienteService.RemoverAsync(id);
            if (!sucesso) return NotFound();
            return RedirectToAction(nameof(Index));
        }
    }
}
