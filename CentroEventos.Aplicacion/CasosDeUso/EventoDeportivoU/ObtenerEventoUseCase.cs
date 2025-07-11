
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Repositorios;

namespace CentroEventos.Aplicacion.CasosDeUso.EventoDeportivoU;

public class ObtenerEventoUseCase
{
    private readonly IRepositorioEventoDeportivo _repositorio;

    public ObtenerEventoUseCase(IRepositorioEventoDeportivo repositorio)
    {
        _repositorio = repositorio;
    }

    public async Task<EventoDeportivo?> Ejecutar(int idEvento)
    {
        return await _repositorio.ObtenerPorId(idEvento);
    }

}
