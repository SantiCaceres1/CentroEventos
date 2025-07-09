using System;
using System.Threading.Tasks;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Repositorios;

namespace CentroEventos.Aplicacion.CasosDeUso.EventoDeportivoU;

public class ListarAsistenciaAEventoUseCase
{
    private IRepositorioEventoDeportivo _repoEventos;
    private IRepositorioReserva _repoReservas;

    public ListarAsistenciaAEventoUseCase(
        IRepositorioEventoDeportivo repoEventos,
        IRepositorioReserva repoReservas)
    {
        _repoEventos = repoEventos;
        _repoReservas = repoReservas;
    }

    public List<Reserva> Ejecutar(int idEvento)
    {
        var evento = _repoEventos.ObtenerPorId(idEvento) ??
            throw new EntidadNotFoundException("El evento no existe.");
        if (evento.FechaInicio > DateTime.Now)
            throw new OperacionInvalidaException("Solo se puede listar asistencia para eventos que ya ocurrieron.");
        var reservas = _repoReservas.ListarTodas();
        return reservas
            .Where(r => r.IdEventoDeportivo == idEvento)
            .ToList();
    }
}
