using CentroEventos.Repositorios.Contexto;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Servicios;
using CentroEventos.Aplicacion.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace CentroEventos.Repositorios.Repositorios
{
    public class RepositorioUsuarioEF : IRepositorioUsuario
    {
        private readonly CentroEventosContext _context;

        public RepositorioUsuarioEF()
        {
            _context = new CentroEventosContext();
        }

        public async Task Agregar(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task Modificar(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }
        
        public async Task Eliminar(Usuario usuario)
        {
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<Usuario?> ObtenerPorNombre(string nombreUsuario)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Nombre == nombreUsuario);
        }

        public async Task<Usuario> ObtenerPorCorreoElectronico(string correoElectronico)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.CorreoElectronico == correoElectronico);
        }

        public async Task<bool> ExisteUsuario(string nombreUsuario)
        {
            return await _context.Usuarios.AnyAsync(u => u.Nombre == nombreUsuario);
        }

        public async Task<Usuario?> ObtenerPorId(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<List<Usuario>> ListarTodas()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<bool> ExisteID(int id)
        {
            return await _context.Usuarios.AnyAsync(u => u.ID == id);
        }

        public async Task<bool> ExisteCorreoElectronico(string correoElectronico)
        {
            return await _context.Usuarios.AnyAsync(u => u.CorreoElectronico == correoElectronico);
        }

        /*public async Task<bool> VerificarContraseña(string contraseña)
        {
            string hash = Hasher.Hashear(contraseña);
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.HashContraseña == hash);
            if (usuario != null)
            {
                return true;
            }
            // Si no se encuentra el usuario o la contraseña no coincide, retornar false
            return await Task.FromResult(false);
        }*/

        public async Task<bool> AgregarPermiso(int usuarioId, Permiso permiso)
        {
            var usuario = await _context.Usuarios.Include(u => u.Permisos).FirstOrDefaultAsync(u => u.ID == usuarioId);
            if (usuario == null) return false;
            if (usuario.Permisos.Any(p => p.Permiso == permiso)) return false; // Ya tiene el permiso
            usuario.Permisos.Add(new UsuarioPermiso { UsuarioId = usuarioId, Permiso = permiso });
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarPermiso(int usuarioId, Permiso permiso)
        {
            var usuario = await _context.Usuarios.Include(u => u.Permisos).FirstOrDefaultAsync(u => u.ID == usuarioId);
            if (usuario == null) return false;

            var permisoExistente = usuario.Permisos.FirstOrDefault(p => p.Permiso == permiso);
            if (permisoExistente == null) return false; // No tiene el permiso

            usuario.Permisos.Remove(permisoExistente);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PoseeElPermiso(int usuarioId, Permiso permiso)
        {
            var usuario = await _context.Usuarios.Include(u => u.Permisos).FirstOrDefaultAsync(u => u.ID == usuarioId);
            return usuario?.Permisos?.Any(p => p.Permiso == permiso) ?? false;
        }
    }
}