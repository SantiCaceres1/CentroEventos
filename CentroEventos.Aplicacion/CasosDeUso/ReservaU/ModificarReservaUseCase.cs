using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Repositorios;
using CentroEventos.Aplicacion.Servicios;

public class ModificarReservaUseCase
{
    private readonly IRepositorioReserva _repositorio;
    private readonly IServicioAutorizacion _autorizacion;
    private readonly ValidadorReserva _validador;

    public ModificarReservaUseCase(
        IRepositorioReserva repositorio,
        IServicioAutorizacion autorizacion,
        ValidadorReserva validador)
    {
        _repositorio = repositorio;
        _autorizacion = autorizacion;
        _validador = validador;
    }

    public async Task Ejecutar(Reserva reserva, int idUsuario)
    {
        var permiso = await _autorizacion.PoseeElPermiso(idUsuario, Permiso.ReservaModificacion);
        if (!permiso)
            throw new FalloAutorizacionException("No tiene permiso para modificar reservas.");

        if (!await _repositorio.ExisteReserva(reserva.Id))
            throw new EntidadNotFoundException("La reserva no existe.");

        var errores = await _validador.Validar(reserva);
        if (errores.Any())
            throw new ValidacionException(errores);

        await _repositorio.Modificar(reserva);
    }
}