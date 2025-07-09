using CentroEventos.Repositorios.Contexto;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace CentroEventos.Repositorios.Repositorios
{
    public class RepositorioEventoDeportivoEF : IRepositorioEventoDeportivo
    {
        private CentroEventosContext _context;

        public RepositorioEventoDeportivoEF(CentroEventosContext context)
        {
            _context = context;
        }

        public void Agregar(EventoDeportivo evento)
        {
            _context.Eventos.Add(evento);
            _context.SaveChanges();
        }

        public void Modificar(EventoDeportivo evento)
        {
            var existe = _context.Eventos.Any(e => e.Id == evento.Id);
            if (!existe) return;
            _context.Eventos.Update(evento);
            _context.SaveChanges();
        }

        public void Eliminar(int id)
        {
            var evento = _context.Eventos.Find(id);
            if (evento is not null)
            {
                _context.Eventos.Remove(evento);
                _context.SaveChanges();
            }
        }

        public EventoDeportivo? ObtenerPorId(int id)
        {
            return _context.Eventos.Find(id);
        }

        public List<EventoDeportivo> ListarTodos()
        {
            return _context.Eventos.ToList();
        }

        public bool ExisteId(int id)
        {
            return _context.Eventos.Any(e => e.Id == id);
        }

        public bool HayCupoDisponible(int id)
        {
            var evento = _context.Eventos.Find(id);
            if (evento is null) return false;
            int cantidadReservas = _context.Reservas
                .Count(r => r.IdEventoDeportivo == id);
            return cantidadReservas < evento.CupoMaximo;
        }
    }
}