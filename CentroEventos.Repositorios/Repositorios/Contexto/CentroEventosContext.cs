using Microsoft.EntityFrameworkCore;
using CentroEventos.Aplicacion.Entidades;

namespace CentroEventos.Repositorios.Contexto
{
    public class CentroEventosContext : DbContext
    {

        #nullable disable 
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

        public static void Inicializar()
        {
            Console.WriteLine("Inicializando base de datos...");
            using var context = new CentroEventosContext();
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
            }
        }
}