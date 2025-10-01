using System.ComponentModel.DataAnnotations;

namespace GerenciamentoEventosHoteis.Models
{
    public class Participante
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string NomeCompleto { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(15)]
        public string Telefone { get; set; }

        public bool HospedeHotel { get; set; }

        public int EventoId { get; set; }
        public Evento Evento { get; set; }

        public bool PresencaConfirmada { get; set; }
        public DateTime? DataCancelamento { get; set; }
    }
}
