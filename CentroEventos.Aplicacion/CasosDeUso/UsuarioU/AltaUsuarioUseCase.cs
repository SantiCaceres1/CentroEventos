using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Repositorios;
using CentroEventos.Aplicacion.Servicios;

public class AltaUsuarioUseCase
{
    private readonly IRepositorioUsuario _repositorioUsuario;
    private readonly IServicioAutorizacion _servicioAutorizacion;
    private readonly ValidadorUsuario _validador;

    public AltaUsuarioUseCase(
        IRepositorioUsuario repositorioUsuario,
        IServicioAutorizacion servicioAutorizacion,
        ValidadorUsuario validador)
    {
        _repositorioUsuario = repositorioUsuario;
        _servicioAutorizacion = servicioAutorizacion;
        _validador = validador;
    }

    public async Task Ejecutar(Usuario usuario, int idUsuario)
    {
        var permiso = await _servicioAutorizacion.PoseeElPermiso(idUsuario, Permiso.UsuarioAlta);
        if (!permiso)
            throw new FalloAutorizacionException("El usuario no tiene permiso para dar de alta a usuarios.");

        var errores = await _validador.Validar(usuario);
        if (errores.Any())
            throw new ValidacionException(errores);

        await _repositorioUsuario.Agregar(usuario);
    }
}