using Microsoft.EntityFrameworkCore;
using CentroEventos.Aplicacion.Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Infrastructure;


namespace CentroEventos.Repositorios.Contexto
{
    public class CentroEventosContext : DbContext
    {

#nullable disable
        public DbSet<UsuarioPermiso> UsuarioPermisos { get; set; }
        public DbSet<EventoDeportivo> Eventos { get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
#nullable enable

        public CentroEventosContext()
        {
        }

        public CentroEventosContext(DbContextOptions<CentroEventosContext> options) : base(options)
        {
        }

        public static void Inicializar(CentroEventosContext context)
        {
            Console.WriteLine("Inicializando base de datos...");
            if (context.Database.EnsureCreated())
            {
                var connection = context.Database.GetDbConnection();
                connection.Open();
                using var command = connection.CreateCommand();
                command.CommandText = "PRAGMA journal_mode=DELETE;";
                command.ExecuteNonQuery();
                Console.WriteLine("Se creó base de datos");
            }
            else
            {
                Console.WriteLine("La base de datos ya existía");
            }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("data source=CentroEventos.sqlite");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventoDeportivo>().ToTable("Eventos");
            modelBuilder.Entity<Persona>().ToTable("Personas");
            modelBuilder.Entity<Reserva>().ToTable("Reservas");
            modelBuilder.Entity<Usuario>().ToTable("Usuarios");

            modelBuilder.Entity<EventoDeportivo>().HasKey(e => e.Id); 
            modelBuilder.Entity<Persona>().HasKey(e => e.Id); 
            modelBuilder.Entity<Reserva>().HasKey(e => e.Id); 
            modelBuilder.Entity<Usuario>().HasKey(e => e.ID); 

            modelBuilder.Entity<UsuarioPermiso>()
                .ToTable("UsuarioPermisos")
                .HasKey(up => new { up.UsuarioId, up.Permiso });

            modelBuilder.Entity<UsuarioPermiso>()
                .HasOne(up => up.Usuario)
                .WithMany(u => u.Permisos)
                .HasForeignKey(up => up.UsuarioId);
        }

            
        }
}