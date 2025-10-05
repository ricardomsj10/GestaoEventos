using System.ComponentModel.DataAnnotations;

namespace GerenciamentoEventosHoteis.Models
{
    public class Auditoria
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo Usuário é obrigatório.")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "O campo Ação é obrigatório.")]
        public string Acao { get; set; }

        [Required(ErrorMessage = "O campo Data e Hora é obrigatório.")]
        public DateTime DataHora { get; set; }

        [Required(ErrorMessage = "O campo Entidade é obrigatório.")]
        public string Entidade { get; set; }

        [Required(ErrorMessage = "O campo EntidadeId é obrigatório.")]
        public int? EntidadeId { get; set; }

        [Required(ErrorMessage = "O campo Dados Anteriores é obrigatório.")]
        public string DadosAnteriores { get; set; }

        [Required(ErrorMessage = "O campo Dados Novos é obrigatório.")]
        public string DadosNovos { get; set; }
    }
}
