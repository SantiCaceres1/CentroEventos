using CentroEventos.Aplicacion.Repositorios;
using CentroEventos.Repositorios.Contexto;
using Microsoft.EntityFrameworkCore;
using CentroEventos.Repositorios.Repositorios;
using CentroEventos.UI.Components;
using CentroEventos.Aplicacion.Servicios;
using CentroEventos.Aplicacion.CasosDeUso.EventoDeportivoU;
using CentroEventos.Aplicacion.CasosDeUso.ReservaU;
using CentroEventos.Aplicacion.Validadores;
using CentroEventos.Aplicacion.CasosDeUso.UsuarioU;
using CentroEventos.Aplicacion.CasosDeUso.PersonaU;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents(); 

// Inyección de dependencias - Casos de Uso
builder.Services.AddTransient<AltaEventoDeportivoUseCase>();
builder.Services.AddTransient<ListarEventoDeportivoUseCase>();
builder.Services.AddTransient<ModificarEventoDeportivoUseCase>();
builder.Services.AddTransient<ListarEventosConCupoDisponibleUseCase>();

builder.Services.AddTransient<AltaUsuarioUseCase>();
builder.Services.AddTransient<EliminarUsuarioUseCase>();
builder.Services.AddTransient<ListarUsuarioUseCase>();
builder.Services.AddTransient<ModificarUsuarioUseCase>();
builder.Services.AddTransient<AltaUsuarioUseCase>();

builder.Services.AddTransient<AltaReservaUseCase>();
builder.Services.AddTransient<ListarReservaUseCase>();

builder.Services.AddTransient<IniciarSesionUseCase>();
builder.Services.AddTransient<UsuarioEsAdminUseCase>();
builder.Services.AddTransient<RegistrarUsuarioUseCase>();
builder.Services.AddTransient<AltaEventoDeportivoUseCase>();
builder.Services.AddTransient<ListarEventoDeportivoUseCase>();
builder.Services.AddTransient<ModificarEventoDeportivoUseCase>();
builder.Services.AddTransient<EliminarEventoDeportivoUseCase>();
builder.Services.AddTransient<ObtenerUsuarioUseCase>();
builder.Services.AddTransient<ObtenerEventoUseCase>();
builder.Services.AddTransient<AltaEventoDeportivoUseCase>();
builder.Services.AddTransient<ListarPersonasUseCase>();
builder.Services.AddTransient<EliminarPersonaUseCase>();
builder.Services.AddTransient<AltaPersonaUseCase>();
builder.Services.AddTransient<ListarPersonasUseCase>();
builder.Services.AddTransient<ModificarPersonaUseCase>();
builder.Services.AddTransient<ValidadorPersona>();
builder.Services.AddTransient<AltaReservaUseCase>();
builder.Services.AddTransient<ModificarReservaUseCase>();
builder.Services.AddTransient<EliminarReservaUseCase>();
builder.Services.AddTransient<ListarReservaUseCase>();


// Inyección de dependencias - Repositorios
builder.Services.AddScoped<IRepositorioEventoDeportivo, RepositorioEventoDeportivoEF>();
builder.Services.AddScoped<IRepositorioReserva, RepositorioReservaEF>();
builder.Services.AddScoped<IRepositorioUsuario, RepositorioUsuarioEF>();
builder.Services.AddScoped<IServicioAutorizacion, ServicioAutorizacion>();
builder.Services.AddScoped<IRepositorioPersona, RepositorioPersonaEF>();
builder.Services.AddScoped<ServicioAutenticacion>();


// builder.Services.AddScoped<UsuarioSesion>();
builder.Services.AddSingleton<UsuarioSesion>();
/*
    El servicio UsuarioSesion se registra como Singleton porque queremos que la misma instancia viva 
    durante toda la vida de la aplicación. Si bien esto es peligroso porque todos los usuarios comparten
    la misma sesión, en este caso es intencional para que la sesion persista ya que estaremos inyectando
    mal UsuarioSesion en alguna parte de nuestro codigo, haciendo asi que se rompa el ciclo de vida Scoped
    y no manteniendo la misma instancia (persistencia de la sesion).
 */ 

// Inyección de dependencias - Validadores
builder.Services.AddScoped<ValidadorEventoDeportivo>();
builder.Services.AddScoped<ValidadorReserva>();


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
