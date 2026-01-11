using Clientes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clientes.Application.DTOs
{
    public class TelefoneDto
    {
        public int Id { get; set; }
        public int CodigoCliente { get; set; }
        public string? NumeroTelefone { get; set; }
        public int CodigoTipoTelefone { get; set; }
        public string? Operadora { get; set; }
        public bool Ativo { get; set; }
        public string? DescricaoTipoTelefone { get; set; }

        public TipoTelefoneDto TipoTelefone { get; set; } = new TipoTelefoneDto();
    }
}
