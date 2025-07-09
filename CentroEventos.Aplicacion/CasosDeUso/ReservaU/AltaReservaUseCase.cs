using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Repositorios;
using CentroEventos.Aplicacion.Servicios;

public class AltaReservaUseCase
{
    private readonly IRepositorioReserva _repositorioReserva;
    private readonly IServicioAutorizacion _autorizacion;
    private readonly ValidadorReserva _validador;

    public AltaReservaUseCase(
        IRepositorioReserva repositorioReserva,
        IServicioAutorizacion autorizacion,
        ValidadorReserva validador)
    {
        _repositorioReserva = repositorioReserva;
        _autorizacion = autorizacion;
        _validador = validador;
    }

    public async Task Ejecutar(Reserva reserva, int idUsuario)
    {
        var permiso = await _autorizacion.PoseeElPermiso(idUsuario, Permiso.ReservaAlta);
        if (!permiso)
            throw new FalloAutorizacionException("No tiene permiso para registrar reservas.");

        var errores = await _validador.Validar(reserva);
        if (errores.Any())
            throw new ValidacionException(errores);

        reserva.AsignarFechaDeAlta(DateTime.Now);
        reserva.AsignarEstadoAsistencia(EstadoAsistencia.Pendiente);

        await _repositorioReserva.Agregar(reserva);
    }
}