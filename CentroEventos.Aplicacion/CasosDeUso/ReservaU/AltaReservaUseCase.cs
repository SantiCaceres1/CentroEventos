
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Repositorios;
using CentroEventos.Aplicacion.Servicios;
using CentroEventos.Aplicacion.Validadores;

namespace CentroEventos.Aplicacion.CasosDeUso.ReservaU;

public class AltaReservaUseCase
{
    private readonly IRepositorioReserva _repositorioReserva;
    private readonly IRepositorioEventoDeportivo _repositorioEvento;
    private readonly IRepositorioPersona _repositorioPersona;
    private readonly IServicioAutorizacion _autorizacion;
    private readonly ValidadorReserva _validador;

    public AltaReservaUseCase(IRepositorioReserva repositorioReserva, IRepositorioEventoDeportivo repositorioEvento, IRepositorioPersona repositorioPersona, IServicioAutorizacion autorizacion)
    {
        _repositorioReserva = repositorioReserva;
        _repositorioEvento = repositorioEvento;
        _repositorioPersona = repositorioPersona;
        _autorizacion = autorizacion;
        _validador = new ValidadorReserva(repositorioReserva, repositorioPersona, repositorioEvento);
    }

    public async void Ejecutar(Reserva reserva, int idUsuario)
    {
        var permiso = await _autorizacion.PoseeElPermiso(idUsuario, Permiso.ReservaAlta);
        if (!permiso)
            throw new FalloAutorizacionException("No tiene permiso para registgar reservas.");
        var errores = await _validador.Validar(reserva);
        if (errores.Any())
            throw new ExcepcionValidacion("Errores de validación al dar de alta la reserva.", errores);

        reserva.AsignarFechaDeAlta(DateTime.Now);

        reserva.AsignarEstadoAsistencia(EstadoAsistencia.Pendiente);

        await _repositorioReserva.Agregar(reserva);
    }



}
