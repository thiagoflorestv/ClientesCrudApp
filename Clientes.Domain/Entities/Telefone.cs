using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clientes.Domain.Entities
{
    public class Telefone
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CodigoCliente { get; set; }

        [Required, MaxLength(20)]
        public string? NumeroTelefone { get; set; }

        [Required]
        public int CodigoTipoTelefone { get; set; }

        [MaxLength(50)]
        public string? Operadora { get; set; }

        public bool Ativo { get; set; }

        public DateTime DataInsercao { get; set; }
        public string? UsuarioInsercao { get; set; }

        public Cliente? Cliente { get; set; }
        public TipoTelefone TipoTelefone { get; set; } = new TipoTelefone();
    }


}
