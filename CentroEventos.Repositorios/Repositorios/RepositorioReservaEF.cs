using CentroEventos.Repositorios.Contexto;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace CentroEventos.Repositorios.Repositorios
{
    public class RepositorioReservaEF : IRepositorioReserva
    {
        private CentroEventosContext _context;

        public RepositorioReservaEF(CentroEventosContext context)
        {
            _context = context;
        }

        public void Agregar(Reserva reserva)
        {
            _context.Reservas.Add(reserva);
            _context.SaveChanges();
        }

        public void Modificar(Reserva reserva)
        {
            var existe = _context.Reservas.Any(r => r.Id == reserva.Id);
            if (!existe) return;
            _context.Reservas.Update(reserva);
            _context.SaveChanges();
        }

        public void Eliminar(int id)
        {
            var reserva = _context.Reservas.Find(id);
            if (reserva is not null)
            {
                _context.Reservas.Remove(reserva);
                _context.SaveChanges();
            }
        }

        public Reserva? ObtenerPorId(int id)
        {
            return _context.Reservas.Find(id);
        }

        public List<Reserva> ListarTodas()
        {
            return _context.Reservas.ToList();
        }

        public bool ExisteReserva(int id)
        {
            return _context.Reservas.Any(r => r.Id == id);
        }

        public bool ExisteReservaDuplicada(int personaId, int eventoId)
        {
            return _context.Reservas.Any(r =>
                r.IdPersona == personaId && r.IdEventoDeportivo == eventoId);
        }

        public int ContarReservaParaEvento(int eventoId)
        {
            return _context.Reservas.Count(r => r.IdEventoDeportivo == eventoId);
        }
    }
}