using System.ComponentModel.DataAnnotations;

namespace GerenciamentoEventosHoteis.Models
{
    public class Auditoria
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Acao { get; set; }
        public DateTime DataHora { get; set; }
        public string Entidade { get; set; }
        public int? EntidadeId { get; set; }
        public string DadosAnteriores { get; set; }
        public string DadosNovos { get; set; }
    }
}
