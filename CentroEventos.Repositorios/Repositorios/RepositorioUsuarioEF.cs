using CentroEventos.Repositorios.Contexto;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Servicios;
using CentroEventos.Aplicacion.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace CentroEventos.Repositorios.Repositorios
{
    public class RepositorioUsuarioEF : IRepositorioUsuario
    {
        private CentroEventosContext _context;

        public RepositorioUsuarioEF(CentroEventosContext context)
        {
            _context = context;
        }

        public void Agregar(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        public void Modificar(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            _context.SaveChanges();
        }

        public void Eliminar(Usuario usuario)
        {
            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
        }

        public Usuario? ObtenerPorNombre(string nombreUsuario)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Nombre == nombreUsuario);
        }

        public Usuario? ObtenerPorCorreoElectronico(string correoElectronico)
        {
            return _context.Usuarios.FirstOrDefault(u => u.CorreoElectronico == correoElectronico);
        }

        public bool ExisteUsuario(string nombreUsuario)
        {
            return _context.Usuarios.Any(u => u.Nombre == nombreUsuario);
        }

        public Usuario? ObtenerPorId(int id)
        {
            return _context.Usuarios.Find(id);
        }

        public List<Usuario> ListarTodas()
        {
            return _context.Usuarios.ToList();
        }

        public bool ExisteID(int id)
        {
            return _context.Usuarios.Any(u => u.Id == id);
        }

        public bool ExisteCorreoElectronico(string correoElectronico)
        {
            return _context.Usuarios.Any(u => u.CorreoElectronico == correoElectronico);
        }

        public bool AgregarPermiso(int usuarioId, Permiso permiso)
        {
            var usuario = _context.Usuarios.Include(u => u.Permisos).FirstOrDefault(u => u.Id == usuarioId);
            if (usuario == null) return false;
            if (usuario.Permisos.Any(p => p.Permiso == permiso)) return false; // Ya tiene el permiso
            usuario.Permisos.Add(new UsuarioPermiso { UsuarioId = usuarioId, Permiso = permiso });
            _context.SaveChanges();
            return true;
        }

        public bool EliminarPermiso(int usuarioId, Permiso permiso)
        {
            var usuario = _context.Usuarios.Include(u => u.Permisos).FirstOrDefault(u => u.Id == usuarioId);
            if (usuario == null) return false;
            var permisoExistente = usuario.Permisos.FirstOrDefault(p => p.Permiso == permiso);
            if (permisoExistente == null) return false; // No tiene el permiso
            usuario.Permisos.Remove(permisoExistente);
            _context.SaveChanges();
            return true;
        }

        public bool PoseeElPermiso(int usuarioId, Permiso permiso)
        {
            var usuario = _context.Usuarios.Include(u => u.Permisos).FirstOrDefault(u => u.Id == usuarioId);
            return usuario?.Permisos?.Any(p => p.Permiso == permiso) ?? false;
        }
    }
}