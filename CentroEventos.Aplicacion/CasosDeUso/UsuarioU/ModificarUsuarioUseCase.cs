using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Repositorios;
using CentroEventos.Aplicacion.Servicios;

public class ModificarUsuarioUseCase
{
    private readonly IRepositorioUsuario _repositorio;
    private readonly IServicioAutorizacion _autorizacion;
    private readonly ValidadorUsuario _validador;

    public ModificarUsuarioUseCase(
        IRepositorioUsuario repositorio,
        IServicioAutorizacion autorizacion,
        ValidadorUsuario validador)
    {
        _repositorio = repositorio;
        _autorizacion = autorizacion;
        _validador = validador;
    }

    public async Task Ejecutar(Usuario usuario, int idAdmin)
    {
        var permiso = await _autorizacion.PoseeElPermiso(idAdmin, Permiso.UsuarioModificacion);
        if (!permiso)
            throw new FalloAutorizacionException("El usuario no tiene permiso para modificar personas.");

        if (!await _repositorio.ExisteID(usuario.Id))
            throw new EntidadNotFoundException("No existe el usuario que se quiere modificar.");

        var errores = await _validador.Validar(usuario);
        if (errores.Any())
            throw new ValidacionException(errores);

        await _repositorio.Modificar(usuario);
    }
}