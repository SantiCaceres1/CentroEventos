using CentroEventos.Aplicacion.Entidades; 
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Repositorios;
using CentroEventos.Aplicacion.Servicios;

namespace CentroEventos.Aplicacion.CasosDeUso.UsuarioU;

public class AsignarPermisoUsuarioUseCase 
{
    private readonly IRepositorioUsuario _repositorio;
    private readonly UsuarioSesion _usuarioSesion;
    public AsignarPermisoUsuarioUseCase(IRepositorioUsuario repositorio, UsuarioSesion usuarioSesion)
    {
        _repositorio = repositorio;
        _usuarioSesion = usuarioSesion;
    }

    public async Task Ejecutar(int idEditor, int idUsuario, Permiso permiso)
    {
        var editor = await _repositorio.ObtenerPorId(idEditor) ?? throw new EntidadNotFoundException("Usuario editor no encontrado");
        var usuario = await _repositorio.ObtenerPorId(idUsuario) ?? throw new EntidadNotFoundException("Usuario destino no encontrado");
        var esAdmin = await _usuarioSesion.UsuarioEsAdmin();
        if (!esAdmin)
            throw new FalloAutorizacionException("No tiene permisos para asignar permisos a otros usuarios");

        await _repositorio.AgregarPermiso(usuario.ID, permiso);
    }

}