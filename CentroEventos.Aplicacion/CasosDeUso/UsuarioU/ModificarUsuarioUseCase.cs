using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Repositorios;
using CentroEventos.Aplicacion.Servicios;

public class ModificarUsuarioUseCase
{
    private IRepositorioUsuario _repositorio;
    private IServicioAutorizacion _autorizacion;
    private ValidadorUsuario _validador;

    public ModificarUsuarioUseCase(
        IRepositorioUsuario repositorio,
        IServicioAutorizacion autorizacion,
        ValidadorUsuario validador)
    {
        _repositorio = repositorio;
        _autorizacion = autorizacion;
        _validador = validador;
    }

    public void Ejecutar(Usuario usuario, int idAdmin)
    {
        var permiso = _autorizacion.PoseeElPermiso(idAdmin, Permiso.UsuarioModificacion);
        if (!permiso)
            throw new FalloAutorizacionException("El usuario no tiene permiso para modificar personas.");
        if (!_repositorio.ExisteID(usuario.Id))
            throw new EntidadNotFoundException("No existe el usuario que se quiere modificar.");
        var errores = _validador.Validar(usuario);
        if (errores.Any())
            throw new ValidacionException(errores);
        _repositorio.Modificar(usuario);
    }
}