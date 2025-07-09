using CentroEventos.Repositorios.Contexto;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace CentroEventos.Repositorios.Repositorios
{
    public class RepositorioEventoDeportivoEF : IRepositorioEventoDeportivo
    {
        private readonly CentroEventosContext _context;

        public RepositorioEventoDeportivoEF(CentroEventosContext context)
        {
            _context = context;
        }

        public async Task Agregar(EventoDeportivo evento)
        {
            await _context.Eventos.AddAsync(evento);
            await _context.SaveChangesAsync();
        }

        public async Task Modificar(EventoDeportivo evento)
        {
            var existe = await _context.Eventos.AnyAsync(e => e.Id == evento.Id);
            if (!existe) return;
            _context.Eventos.Update(evento);
            await _context.SaveChangesAsync();
        }

        public async Task Eliminar(int id)
        {
            var evento = await _context.Eventos.FindAsync(id);
            if (evento is not null)
            {
                _context.Eventos.Remove(evento);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<EventoDeportivo?> ObtenerPorId(int id)
        {
            return await _context.Eventos.FindAsync(id);
        }

        public async Task<List<EventoDeportivo>> ListarTodos()
        {
            return await _context.Eventos.ToListAsync();
        }

        public async Task<bool> ExisteId(int id)
        {
            return await _context.Eventos.AnyAsync(e => e.Id == id);
        }

        public async Task<bool> HayCupoDisponible(int id)
        {
            var evento = await _context.Eventos.FindAsync(id);
            if (evento is null) return false;

            int cantidadReservas = await _context.Reservas
                .CountAsync(r => r.IdEventoDeportivo == id);

            return cantidadReservas < evento.CupoMaximo;
        }
    }
}