using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using GerenciamentoEventosHoteis.Data;
using GerenciamentoEventosHoteis.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("EventosHoteisDB"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IAuditoriaService, AuditoriaService>();

var app = builder.Build();

// Seed data fictício
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<GerenciamentoEventosHoteis.Data.ApplicationDbContext>();
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    // Perfis
    var roles = new[] { "Admin", "Organizador", "Participante" };
    foreach (var role in roles)
    {
        if (!roleManager.RoleExistsAsync(role).Result)
            roleManager.CreateAsync(new IdentityRole(role)).Wait();
    }

    // Usuários de teste
    var users = new[] {
        new { UserName = "admin@hotel.com", Password = "Admin123!", Role = "Admin" },
        new { UserName = "org@hotel.com", Password = "Org123!", Role = "Organizador" },
        new { UserName = "part@hotel.com", Password = "Part123!", Role = "Participante" }
    };
    foreach (var u in users)
    {
        if (userManager.FindByNameAsync(u.UserName).Result == null)
        {
            var user = new IdentityUser { UserName = u.UserName, Email = u.UserName, EmailConfirmed = true };
            userManager.CreateAsync(user, u.Password).Wait();
            userManager.AddToRoleAsync(user, u.Role).Wait();
        }
    }

    // Seed de hotéis
    if (!context.Hoteis.Any())
    {
        context.Hoteis.AddRange(new[] {
            new GerenciamentoEventosHoteis.Models.Hotel { Nome = "Hotel Copacabana", Endereco = "Av. Atlântica, 1702", CNPJ = "12.345.678/0001-99", ContatoPrincipal = "contato@copacabana.com", Ativo = true },
            new GerenciamentoEventosHoteis.Models.Hotel { Nome = "Hotel Paulista", Endereco = "Av. Paulista, 1000", CNPJ = "98.765.432/0001-11", ContatoPrincipal = "contato@paulista.com", Ativo = true },
            new GerenciamentoEventosHoteis.Models.Hotel { Nome = "Hotel Inativo", Endereco = "Rua Fictícia, 123", CNPJ = "11.111.111/0001-22", ContatoPrincipal = "contato@inativo.com", Ativo = false }
        });
        context.SaveChanges();
    }

    // Seed de eventos
    if (!context.Eventos.Any())
    {
        var hotel1 = context.Hoteis.First(h => h.Nome == "Hotel Copacabana");
        var hotel2 = context.Hoteis.First(h => h.Nome == "Hotel Paulista");
        context.Eventos.AddRange(new[] {
            new GerenciamentoEventosHoteis.Models.Evento {
                Nome = "Congresso de TI",
                DataInicio = DateTime.Today.AddDays(10),
                DataTermino = DateTime.Today.AddDays(12),
                Localizacao = "Salão Azul",
                CapacidadeMaxima = 100,
                Descricao = "Evento de tecnologia para profissionais do setor.",
                Pago = true,
                Valor = 500,
                HotelId = hotel1.Id
            },
            new GerenciamentoEventosHoteis.Models.Evento {
                Nome = "Workshop de Gastronomia",
                DataInicio = DateTime.Today.AddDays(20),
                DataTermino = DateTime.Today.AddDays(21),
                Localizacao = "Restaurante Central",
                CapacidadeMaxima = 50,
                Descricao = "Aulas práticas com chefs renomados.",
                Pago = false,
                HotelId = hotel2.Id
            }
        });
        context.SaveChanges();
    }

    // Seed de participantes
    if (!context.Participantes.Any())
    {
        var evento1 = context.Eventos.First(e => e.Nome == "Congresso de TI");
        var evento2 = context.Eventos.First(e => e.Nome == "Workshop de Gastronomia");
        context.Participantes.AddRange(new[] {
            new GerenciamentoEventosHoteis.Models.Participante {
                NomeCompleto = "Carlos Silva",
                Email = "carlos@exemplo.com",
                Telefone = "(11) 91234-5678",
                HospedeHotel = true,
                EventoId = evento1.Id,
                PresencaConfirmada = false
            },
            new GerenciamentoEventosHoteis.Models.Participante {
                NomeCompleto = "Ana Souza",
                Email = "ana@exemplo.com",
                Telefone = "(21) 92345-6789",
                HospedeHotel = false,
                EventoId = evento2.Id,
                PresencaConfirmada = false
            }
        });
        context.SaveChanges();
    }

    // Seed de pagamentos
    if (!context.Pagamentos.Any())
    {
        var evento1 = context.Eventos.First(e => e.Nome == "Congresso de TI");
        var participante1 = context.Participantes.First(p => p.NomeCompleto == "Carlos Silva");
        context.Pagamentos.Add(new GerenciamentoEventosHoteis.Models.Pagamento {
            ParticipanteId = participante1.Id,
            EventoId = evento1.Id,
            Valor = 500,
            DataPagamento = DateTime.Today.AddDays(1),
            Aprovado = true
        });
        context.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
