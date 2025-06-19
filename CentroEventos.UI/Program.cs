using CentroEventos.Aplicacion.Repositorios;
using CentroEventos.Repositorios.Repositorios;
using CentroEventos.UI.Components;
using CentroEventos.Aplicacion.Servicios;
using CentroEventos.Aplicacion.CasosDeUso.EventoDeportivoU;


var builder = WebApplication.CreateBuilder(args);
 
 //debo agregar una linea Inicializar para crear la base de datos si no existe


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddScoped<IRepositorioEventoDeportivo, RepositorioEventoDeportivoEF>();
builder.Services.AddScoped<IRepositorioReserva, RepositorioReservaEF>();
builder.Services.AddScoped<UsuarioSesion>();
builder.Services.AddScoped<IRepositorioUsuario, RepositorioUsuarioEF>();
builder.Services.AddTransient<ListarEventosConCupoDisponibleUseCase>();

var app = builder.Build();

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
