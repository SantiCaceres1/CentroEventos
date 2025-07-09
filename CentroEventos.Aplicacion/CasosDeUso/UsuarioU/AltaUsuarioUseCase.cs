using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Repositorios;
using CentroEventos.Aplicacion.Servicios;

public class AltaUsuarioUseCase
{
    private IRepositorioUsuario _repositorioUsuario;
    private IServicioAutorizacion _servicioAutorizacion;
    private ValidadorUsuario _validador;

    public AltaUsuarioUseCase(
        IRepositorioUsuario repositorioUsuario,
        IServicioAutorizacion servicioAutorizacion,
        ValidadorUsuario validador)
    {
        _repositorioUsuario = repositorioUsuario;
        _servicioAutorizacion = servicioAutorizacion;
        _validador = validador;
    }

    public void Ejecutar(Usuario usuario, int idUsuario)
    {
        var permiso = _servicioAutorizacion.PoseeElPermiso(idUsuario, Permiso.UsuarioAlta);
        if (!permiso)
            throw new FalloAutorizacionException("El usuario no tiene permiso para dar de alta a usuarios.");
        var errores = _validador.Validar(usuario);
        if (errores.Any())
            throw new ValidacionException(errores);
        _repositorioUsuario.Agregar(usuario);
    }
}