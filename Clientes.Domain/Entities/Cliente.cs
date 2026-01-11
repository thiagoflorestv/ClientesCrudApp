using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clientes.Domain.Entities
{
    public class Cliente
    {
        [Key]
        public int CodigoCliente { get; set; }

        [Required, MaxLength(150)]
        public string? RazaoSocial { get; set; }

        [MaxLength(150)]
        public string? NomeFantasia { get; set; }

        [Required, MaxLength(20)]
        public string? TipoPessoa { get; set; } 

        [Required, MaxLength(20)]
        public string? Documento { get; set; } 

        [MaxLength(200)]
        public string? Endereco { get; set; }

        [MaxLength(100)]
        public string? Complemento { get; set; }

        [MaxLength(100)]
        public string? Bairro { get; set; }

        [MaxLength(100)]
        public string? Cidade { get; set; }

        [MaxLength(8)]
        public string? CEP { get; set; }

        [MaxLength(2)]
        public string? UF { get; set; }

        public DateTime DataInsercao { get; set; }
        public string? UsuarioInsercao { get; set; }

        public ICollection<Telefone> Telefones { get; set; } = new List<Telefone>();

    }

}
