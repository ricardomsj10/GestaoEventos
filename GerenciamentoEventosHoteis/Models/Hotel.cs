using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace GerenciamentoEventosHoteis.Models
{
    public class Hotel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [StringLength(200)]
        public string Endereco { get; set; }

        [Required]
        [StringLength(18)]
        public string CNPJ { get; set; }

        [Required]
        [StringLength(100)]
        public string ContatoPrincipal { get; set; }

        public bool Ativo { get; set; }

        public ICollection<Evento> Eventos { get; set; }
    }
}
