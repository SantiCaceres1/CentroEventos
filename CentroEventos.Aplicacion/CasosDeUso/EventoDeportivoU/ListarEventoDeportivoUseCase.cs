
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Repositorios;

namespace CentroEventos.Aplicacion.CasosDeUso.EventoDeportivoU;

public class ListarEventoDeportivoUseCase
{
    private readonly IRepositorioEventoDeportivo _repositorio;

    public ListarEventoDeportivoUseCase(IRepositorioEventoDeportivo repositorio)
    {
        _repositorio = repositorio;
    }

    public List<EventoDeportivo> Ejecutar()
    {
        return _repositorio.ListarTodos();
    }

}
