using CentroEventos.Repositorios.Contexto;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace CentroEventos.Repositorios.Repositorios
{
    public class RepositorioReservaEF : IRepositorioReserva
    {
        private readonly CentroEventosContext _context;

        public RepositorioReservaEF()
        {
            _context = new CentroEventosContext();
        }

        public async Task Agregar(Reserva reserva)
        {
            await _context.Reservas.AddAsync(reserva);
            await _context.SaveChangesAsync();
        }

        public async Task Modificar(Reserva reserva)
        {
            var existe = await _context.Reservas.AnyAsync(r => r.Id == reserva.Id);
            if (!existe) return;
            _context.Reservas.Update(reserva);
            await _context.SaveChangesAsync();
        }

        public async Task Eliminar(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva is not null)
            {
                _context.Reservas.Remove(reserva);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Reserva?> ObtenerPorId(int id)
        {
            return await _context.Reservas.FindAsync(id);
        }

        public async Task<List<Reserva>> ListarTodas()
        {
            return await _context.Reservas.ToListAsync();
        }

        public async Task<bool> ExisteReserva(int id)
        {
            return await _context.Reservas.AnyAsync(r => r.Id == id);
        }

        public async Task<bool> ExisteReservaDuplicada(int personaId, int eventoId)
        {
            return await _context.Reservas.AnyAsync(r =>
                r.IdPersona == personaId && r.IdEventoDeportivo == eventoId);
        }

        public async Task<int> ContarReservaParaEvento(int eventoId)
        {
            return await _context.Reservas.CountAsync(r => r.IdEventoDeportivo == eventoId);
        }
    }
}