using CentroEventos.Repositorios.Contexto;
using CentroEventos.Aplicacion.Entidades;
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

        public async Task<Usuario?> ObtenerPorCorreo(string correoElectronico)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.CorreoElectronico == correoElectronico);
        }

        public async Task<bool> ExisteUsuario(string nombreUsuario)
        {
            return await _context.Usuarios.AnyAsync(u => u.Nombre == nombreUsuario);
        }

        public async Task<bool> EsAdmin(string nombreUsuario)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Nombre == nombreUsuario);
            return usuario?.EsAdmin ?? false;
        }
    }
}