using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using GerenciamentoEventosHoteis.Data;
using GerenciamentoEventosHoteis.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoEventosHoteis.Services
{
    public interface IAuditoriaService
    {
        Task<Auditoria> CreateAsync(Auditoria auditoria);
        Task<Auditoria> UpdateAsync(Auditoria auditoria);
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

        public async Task<Entity> UpdateAsync(Entity entity)
        {
            Validate(entity);
            var existing = await Load(entity.Id);
            ApplyChanges(existing, entity);
            await _context.SaveChangesAsync();
            return existing;
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
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ValidationException($"O campo {fieldName} deve ser preenchido.");
            }
        }

        private async Task<Auditoria> LoadAuditoria(int id)
        {
            var existing = await _context.Auditorias.FirstOrDefaultAsync(a => a.Id == id);
            if (existing == null)
            {
                throw new KeyNotFoundException("Auditoria não encontrada.");
            }
            return existing;
        }

        private static void ApplyChanges(Auditoria target, Auditoria source)
        {
            target.Usuario = source.Usuario;
            target.Acao = source.Acao;
            target.DataHora = source.DataHora;
            target.Entidade = source.Entidade;
            target.EntidadeId = source.EntidadeId;
            target.DadosAnteriores = source.DadosAnteriores;
            target.DadosNovos = source.DadosNovos;
        }
    }
}
