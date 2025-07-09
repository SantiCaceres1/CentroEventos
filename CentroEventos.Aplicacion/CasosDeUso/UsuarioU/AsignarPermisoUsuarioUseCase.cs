using CentroEventos.Aplicacion.Entidades; 
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Repositorios;
using CentroEventos.Aplicacion.Servicios;

namespace CentroEventos.Aplicacion.CasosDeUso.UsuarioU;

public class AsignarPermisoUsuarioUseCase 
{
    private IRepositorioUsuario _repositorio;
    private UsuarioSesion _usuarioSesion;
    public AsignarPermisoUsuarioUseCase(IRepositorioUsuario repositorio, UsuarioSesion usuarioSesion)
    {
        _repositorio = repositorio;
        _usuarioSesion = usuarioSesion;
    }

    public void Ejecutar(int idEditor, int idUsuario, Permiso permiso)
    {
        var editor = _repositorio.ObtenerPorId(idEditor) ?? throw new EntidadNotFoundException("Usuario editor no encontrado");
        var usuario = _repositorio.ObtenerPorId(idUsuario) ?? throw new EntidadNotFoundException("Usuario destino no encontrado");
        var esAdmin = _usuarioSesion.UsuarioEsAdmin();
        if (!esAdmin)
            throw new FalloAutorizacionException("No tiene permisos para asignar permisos a otros usuarios");
        _repositorio.AgregarPermiso(usuario.Id, permiso);
    }

}