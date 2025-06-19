
using CentroEventos.Aplicacion.Entidades;

namespace CentroEventos.Aplicacion.Repositorios;

public interface IRepositorioUsuario
{
    Task Agregar(Usuario usuario);
    Task Modificar(Usuario usuario);
    Task Eliminar(Usuario usuario);
    Task<Usuario?> ObtenerPorNombre(string nombreUsuario);
    Task<Usuario> ObtenerPorCorreoElectronico(string correoElectronico);
    Task<bool> ExisteUsuario(string nombreUsuario);



    // NUEVOS REPOSITORIOS
    Task<Usuario> ObtenerPorId(int id);
    Task<List<Usuario>> ListarTodas();
    Task<bool> ExisteID(int id);
    Task<bool> ExisteCorreoElectronico(string correoElectronico);
    Task<bool> VerificarContraseña(string contraseña);
    Task<bool> AgregarPermiso(int idUsuario, Permiso permiso);
    Task<bool> EliminarPermiso(int idUsuario, Permiso permiso);
    Task<bool> PoseeElPermiso(int idUsuario, Permiso permiso);
}