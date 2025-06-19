using System;
using System.Threading.Tasks;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Repositorios;

namespace CentroEventos.Aplicacion.CasosDeUso.EventoDeportivoU;

public class ListarAsistenciaAEventoUseCase
{
     private readonly IRepositorioEventoDeportivo _repoEventos;
    private readonly IRepositorioReserva _repoReservas;

    public ListarAsistenciaAEventoUseCase(
        IRepositorioEventoDeportivo repoEventos,
        IRepositorioReserva repoReservas)
    {
        _repoEventos = repoEventos;
        _repoReservas = repoReservas;
    }

    public async Task<List<Reserva>> Ejecutar(int idEvento)
    {
        var evento = await _repoEventos.ObtenerPorId(idEvento);
        if (evento == null)
            throw new EntidadNotFoundException("El evento no existe.");

        if (evento.FechaInicio > DateTime.Now)
            throw new OperacionInvalidaException("Solo se puede listar asistencia para eventos que ya ocurrieron.");

        var reservas = await _repoReservas.ListarTodas();
        return reservas
            .Where(r => r.IdEventoDeportivo == idEvento)
            .ToList();
    }
}
