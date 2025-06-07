
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

    public void Ejecutar(Reserva reserva, int idUsuario)
    {
        if (!_autorizacion.PoseeElPermiso(idUsuario, Permiso.ReservaAlta))
            throw new FalloAutorizacionException("No tiene permiso para registgar reservas.");
        _validador.Validar(reserva);

        reserva.AsignarFechaDeAlta(DateTime.Now);

        reserva.AsignarEstadoAsistencia(EstadoAsistencia.Pendiente);

        _repositorioReserva.Agregar(reserva);
    }



}
