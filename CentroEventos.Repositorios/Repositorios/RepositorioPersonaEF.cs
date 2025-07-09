using CentroEventos.Repositorios.Contexto;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace CentroEventos.Repositorios.Repositorios
{
    public class RepositorioPersonaEF : IRepositorioPersona
    {
        private CentroEventosContext _context;

        public RepositorioPersonaEF(CentroEventosContext context)
        {
            _context = context;
        }

        public void Agregar(Persona persona)
        {
            _context.Personas.Add(persona);
            _context.SaveChanges();
        }

        public void Modificar(Persona persona)
        {
            var existe = _context.Personas.Any(p => p.Id == persona.Id);
            if (!existe) return;
            _context.Personas.Update(persona);
            _context.SaveChanges();
        }

        public void Eliminar(int id)
        {
            var persona = _context.Personas.Find(id);
            if (persona != null)
            {
                _context.Personas.Remove(persona);
                _context.SaveChanges();
            }
        }

        public Persona? ObtenerPorId(int id)
        {
            return _context.Personas.Find(id);
        }

        public Persona? ObtenerPorDni(string dni)
        {
            return _context.Personas.FirstOrDefault(p => p.Dni == dni);
        }

        public Persona? ObtenerPorEmail(string email)
        {
            return _context.Personas.FirstOrDefault(p => p.Email == email);
        }

        public List<Persona> ListarTodas()
        {
            return _context.Personas.ToList();
        }

        public bool ExisteDni(string dni)
        {
            return _context.Personas.Any(p => p.Dni == dni);
        }
        public bool ExisteEmail(string email)
        {
            return _context.Personas.Any(p => p.Email == email);
        }
    }
}