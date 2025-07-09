using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Repositorios;
using CentroEventos.Aplicacion.Servicios;

public class ModificarReservaUseCase
{
    private IRepositorioReserva _repositorio;
    private IServicioAutorizacion _autorizacion;
    private ValidadorReserva _validador;

    public ModificarReservaUseCase(
        IRepositorioReserva repositorio,
        IServicioAutorizacion autorizacion,
        ValidadorReserva validador)
    {
        _repositorio = repositorio;
        _autorizacion = autorizacion;
        _validador = validador;
    }

    public void Ejecutar(Reserva reserva, int idUsuario)
    {
        var permiso = _autorizacion.PoseeElPermiso(idUsuario, Permiso.ReservaModificacion);
        if (!permiso)
            throw new FalloAutorizacionException("No tiene permiso para modificar reservas.");
        if (!_repositorio.ExisteReserva(reserva.Id))
            throw new EntidadNotFoundException("La reserva no existe.");
        var errores = _validador.Validar(reserva);
        if (errores.Any())
            throw new ValidacionException(errores);
        _repositorio.Modificar(reserva);
    }
}