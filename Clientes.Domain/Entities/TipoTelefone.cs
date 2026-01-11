using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clientes.Domain.Entities
{
    public class TipoTelefone
    {
        [Key]
        public int CodigoTipoTelefone { get; set; }

        [Required, MaxLength(50)]
        public string? DescricaoTipoTelefone { get; set; }

        public DateTime DataInsercao { get; set; }
        public string? UsuarioInsercao { get; set; }

        public ICollection<Telefone>? Telefones { get; set; }
    }


}
