using System.ComponentModel.DataAnnotations;

namespace GerenciamentoEventosHoteis.Models
{
    public class Pagamento
    {
        public int Id { get; set; }
        public int ParticipanteId { get; set; }
        public Participante Participante { get; set; }
        public int EventoId { get; set; }
        public Evento Evento { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataPagamento { get; set; }
        public bool Aprovado { get; set; }
    }
}
