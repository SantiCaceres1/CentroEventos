using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Repositorios;
using CentroEventos.Aplicacion.Servicios;

namespace CentroEventos.Aplicacion.CasosDeUso.UsuarioU;

public class EliminarPermisoUsuarioUseCase
{
    private readonly IRepositorioUsuario _repositorio;
    private readonly UsuarioEsAdminUseCase _esAdminUseCase;
    public EliminarPermisoUsuarioUseCase(IRepositorioUsuario repositorio, UsuarioEsAdminUseCase esAdminUseCase)
    {
        _repositorio = repositorio;
        _esAdminUseCase = esAdminUseCase;
    }

    public async Task Ejecutar(int idEditor, int idUsuario, Permiso permiso)
    {
        if (await _repositorio.ObtenerPorId(idEditor) == null) throw new Exception("Usuario editor no encontrado");
        var usuario = await _repositorio.ObtenerPorId(idUsuario) ?? throw new Exception("Usuario no encontrado.");
        var esAdmin = await _esAdminUseCase.Ejecutar();
        if (!esAdmin)
            throw new FalloAutorizacionException("No tenés permiso para eliminar permisos de otros usuarios.");

        await _repositorio.EliminarPermiso(usuario.Id, permiso); 
    }
}
