
using CentroEventos.Aplicacion.Entidades;

namespace CentroEventos.Aplicacion.Repositorios;

public interface IRepositorioUsuario
{
    Task Agregar(Usuario usuario);
    Task Modificar(Usuario usuario);
    Task Eliminar(Usuario usuario);
    Task<Usuario?> ObtenerPorNombre(string nombreUsuario);
    Task<Usuario?> ObtenerPorCorreo(string correoElectronico);
    Task<bool> ExisteUsuario(string nombreUsuario);
    Task<bool> EsAdmin(string nombreUsuario);

    // MI CODIGO
    Task<Usuario> ObtenerPorId(int id);
    Task<Usuario> ObtenerPorCorreoElectronico(string CorreoElectronico);
    Task<List<Usuario>> ListarTodas();
    Task<bool> ExisteID(int id);
    Task<bool> ExisteCorreoElectronico(string CorreoElectronico);
    Task<bool> PoseeElPermiso(int idUsuario, Permiso permiso);
    Task<bool> VerificarContraseña(string contraseña);
}