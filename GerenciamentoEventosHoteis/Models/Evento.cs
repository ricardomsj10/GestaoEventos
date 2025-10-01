using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace GerenciamentoEventosHoteis.Models
{
    public class Evento
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        public DateTime DataInicio { get; set; }

        [Required]
        public DateTime DataTermino { get; set; }

        [Required]
        [StringLength(200)]
        public string Localizacao { get; set; }

        [Required]
        public int CapacidadeMaxima { get; set; }

        [Required]
        [StringLength(500)]
        public string Descricao { get; set; }

        public bool Pago { get; set; }
        public decimal? Valor { get; set; }

        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }

        public ICollection<Participante> Participantes { get; set; }
        public ICollection<Pagamento> Pagamentos { get; set; }
    }
}
