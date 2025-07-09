using CentroEventos.Aplicacion.Repositorios;
using CentroEventos.Repositorios.Contexto;
using Microsoft.EntityFrameworkCore;
using CentroEventos.Repositorios.Repositorios;
using CentroEventos.UI.Components;
using CentroEventos.Aplicacion.Servicios;
using CentroEventos.Aplicacion.CasosDeUso.EventoDeportivoU;
using CentroEventos.Aplicacion.CasosDeUso.UsuarioU;
using CentroEventos.Aplicacion.CasosDeUso.ReservaU;
using CentroEventos.Aplicacion.Validadores;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents(); 




// Inyección de dependencias - Casos de Uso
builder.Services.AddTransient<AltaEventoDeportivoUseCase>();
builder.Services.AddTransient<ListarEventoDeportivoUseCase>();
builder.Services.AddTransient<ModificarEventoDeportivoUseCase>();
builder.Services.AddTransient<AltaUsuarioUseCase>();
builder.Services.AddTransient<AltaReservaUseCase>();
builder.Services.AddTransient<ListarReservaUseCase>();
// Inyección de dependencias - Repositorios
builder.Services.AddScoped<IRepositorioEventoDeportivo, RepositorioEventoDeportivoEF>();
builder.Services.AddScoped<IRepositorioReserva, RepositorioReservaEF>();
builder.Services.AddScoped<IRepositorioUsuario, RepositorioUsuarioEF>();
builder.Services.AddScoped<IServicioAutorizacion, ServicioAutorizacion>();
builder.Services.AddScoped<IRepositorioPersona, RepositorioPersonaEF>();
builder.Services.AddScoped<ServicioAutenticacion>();
builder.Services.AddScoped<UsuarioSesion>();

builder.Services.AddTransient<ValidadorUsuario>();

builder.Services.AddDbContext<CentroEventosContext>(options => options.UseSqlite("Data Source=CentroEventos.sqlite"));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CentroEventosContext>();
    CentroEventos.Repositorios.Contexto.CentroEventosContext.Inicializar(context);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
