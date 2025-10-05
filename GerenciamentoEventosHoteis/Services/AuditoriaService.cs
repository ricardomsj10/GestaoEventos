using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using GerenciamentoEventosHoteis.Data;
using GerenciamentoEventosHoteis.Models;

namespace GerenciamentoEventosHoteis.Services
{
    public interface IAuditoriaService
    {
        Task<Auditoria> CreateAsync(Auditoria auditoria);
    }

    public class AuditoriaService : IAuditoriaService
    {
        private readonly ApplicationDbContext _context;

        public AuditoriaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Auditoria> CreateAsync(Auditoria auditoria)
        {
            ValidateAuditoria(auditoria);
            _context.Auditorias.Add(auditoria);
            await _context.SaveChangesAsync();
            return auditoria;
        }

        private static void ValidateAuditoria(Auditoria auditoria)
        {
            ArgumentNullException.ThrowIfNull(auditoria);
            EnsureFilled(auditoria.Usuario, nameof(auditoria.Usuario));
            EnsureFilled(auditoria.Acao, nameof(auditoria.Acao));
            EnsureFilled(auditoria.Entidade, nameof(auditoria.Entidade));
            EnsureFilled(auditoria.DadosAnteriores, nameof(auditoria.DadosAnteriores));
            EnsureFilled(auditoria.DadosNovos, nameof(auditoria.DadosNovos));
            if (!auditoria.EntidadeId.HasValue)
            {
                throw new ValidationException("O campo EntidadeId deve ser preenchido.");
            }
            if (auditoria.DataHora == default)
            {
                throw new ValidationException("O campo DataHora deve ser preenchido.");
            }
        }

        private static void EnsureFilled(string value, string fieldName)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                return;
            }
            throw new ValidationException($"O campo {fieldName} deve ser preenchido.");
        }
    }
}
