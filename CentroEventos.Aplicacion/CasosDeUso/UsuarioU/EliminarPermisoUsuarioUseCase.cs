using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Repositorios;
using CentroEventos.Aplicacion.Servicios;

namespace CentroEventos.Aplicacion.CasosDeUso.UsuarioU;

public class EliminarPermisoUsuarioUseCase
{
    private IRepositorioUsuario _repositorio;
    private UsuarioSesion _usuarioSesion;
    public EliminarPermisoUsuarioUseCase(IRepositorioUsuario repositorio, UsuarioSesion usuarioSesion)
    {
        _repositorio = repositorio;
        _usuarioSesion = usuarioSesion;
    }

    public void Ejecutar(int idEditor, int idUsuario, Permiso permiso)
    {
        if (_repositorio.ObtenerPorId(idEditor) == null) throw new Exception("Usuario editor no encontrado");
        var usuario = _repositorio.ObtenerPorId(idUsuario) ?? throw new Exception("Usuario no encontrado.");
        var esAdmin = _usuarioSesion.UsuarioEsAdmin();
        if (!esAdmin)
            throw new FalloAutorizacionException("No tenés permiso para eliminar permisos de otros usuarios.");
        _repositorio.EliminarPermiso(usuario.Id, permiso);
    }
}
