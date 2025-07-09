using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Repositorios;
using CentroEventos.Aplicacion.Servicios;

public class AltaReservaUseCase
{
    private IRepositorioReserva _repositorioReserva;
    private IServicioAutorizacion _autorizacion;
    private ValidadorReserva _validador;

    public AltaReservaUseCase(
        IRepositorioReserva repositorioReserva,
        IServicioAutorizacion autorizacion,
        ValidadorReserva validador)
    {
        _repositorioReserva = repositorioReserva;
        _autorizacion = autorizacion;
        _validador = validador;
    }

    public void Ejecutar(Reserva reserva, int idUsuario)
    {
        var permiso = _autorizacion.PoseeElPermiso(idUsuario, Permiso.ReservaAlta);
        if (!permiso)
            throw new FalloAutorizacionException("No tiene permiso para registrar reservas.");
        var errores = _validador.Validar(reserva);
        if (errores.Any())
            throw new ValidacionException(errores);
        reserva.AsignarFechaDeAlta(DateTime.Now);
        reserva.AsignarEstadoAsistencia(EstadoAsistencia.Pendiente);
        _repositorioReserva.Agregar(reserva);
    }
}