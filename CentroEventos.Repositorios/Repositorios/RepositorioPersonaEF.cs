using CentroEventos.Repositorios.Contexto;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace CentroEventos.Repositorios.Repositorios
{
    public class RepositorioPersonaEF : IRepositorioPersona
    {
        private readonly CentroEventosContext _context;

        public RepositorioPersonaEF()
        {
            _context = new CentroEventosContext();
        }

        public async Task Agregar(Persona persona)
        {
            await _context.Personas.AddAsync(persona);
            await _context.SaveChangesAsync();
        }

        public async Task Modificar(Persona persona)
        {
            var existe = await _context.Personas.AnyAsync(p => p.Id == persona.Id);
            if (!existe) return;
            _context.Personas.Update(persona);
            await _context.SaveChangesAsync();
        }

        public async Task Eliminar(int id)
        {
            var persona = await _context.Personas.FindAsync(id);
            if (persona != null)
            {
                _context.Personas.Remove(persona);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Persona?> ObtenerPorId(int id)
        {
            return await _context.Personas.FindAsync(id);
        }

        public async Task<Persona?> ObtenerPorDni(string dni)
        {
            return await _context.Personas.FirstOrDefaultAsync(p => p.Dni == dni);
        }

        public async Task<Persona?> ObtenerPorEmail(string email)
        {
            return await _context.Personas.FirstOrDefaultAsync(p => p.Email == email);
        }

        public async Task<List<Persona>> ListarTodas()
        {
            return await _context.Personas.ToListAsync();
        }

        public async Task<bool> ExisteDni(string dni)
        {
            return await _context.Personas.AnyAsync(p => p.Dni == dni);
        }
        public async Task<bool> ExisteEmail(string email)
        {
            return await _context.Personas.AnyAsync(p => p.Email == email);
        }
    }
}