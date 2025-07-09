
using CentroEventos.Aplicacion.Entidades;

namespace CentroEventos.Aplicacion.Repositorios;

public interface IRepositorioUsuario
{
    void Agregar(Usuario usuario);
    void Modificar(Usuario usuario);
    void Eliminar(Usuario usuario);
    Usuario? ObtenerPorNombre(string nombreUsuario);
    Usuario ObtenerPorCorreoElectronico(string correoElectronico);
    bool ExisteUsuario(string nombreUsuario);
    Usuario ObtenerPorId(int id);
    List<Usuario> ListarTodas();
    bool ExisteID(int id);
    bool ExisteCorreoElectronico(string correoElectronico);
    bool AgregarPermiso(int idUsuario, Permiso permiso);
    bool EliminarPermiso(int idUsuario, Permiso permiso);
    bool PoseeElPermiso(int idUsuario, Permiso permiso);
}