using System;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Repositorios;

namespace CentroEventos.Aplicacion.CasosDeUso.EventoDeportivoU;

public class ListarEventosConCupoDisponibleUseCase
{
    private IRepositorioEventoDeportivo _repoEventos;
    private IRepositorioReserva _repoReservas;

    public ListarEventosConCupoDisponibleUseCase(
        IRepositorioEventoDeportivo repoEventos,
        IRepositorioReserva repoReservas)
    {
        _repoEventos = repoEventos;
        _repoReservas = repoReservas;
    }

    public List<EventoDeportivo> Ejecutar()
    {
        var eventos = _repoEventos.ListarTodos()
            .Where(e => e.FechaInicio > DateTime.Now)
            .ToList();
        var reservas = _repoReservas.ListarTodas();
        return eventos
            .Where(e =>
                reservas.Count(r => r.IdEventoDeportivo == e.Id) < e.CupoMaximo)
            .ToList();
    }
}
