using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoEventosHoteis.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<GerenciamentoEventosHoteis.Models.Hotel> Hoteis { get; set; }
    public DbSet<GerenciamentoEventosHoteis.Models.Evento> Eventos { get; set; }
    public DbSet<GerenciamentoEventosHoteis.Models.Participante> Participantes { get; set; }
    public DbSet<GerenciamentoEventosHoteis.Models.Pagamento> Pagamentos { get; set; }
    public DbSet<GerenciamentoEventosHoteis.Models.Auditoria> Auditorias { get; set; }
}
