
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Repositorios;

namespace CentroEventos.Aplicacion.CasosDeUso.ReservaU;

public class ListarReservaUseCase
{
    private readonly IRepositorioReserva _repositorio;

    public ListarReservaUseCase(IRepositorioReserva repositorio)
    {
        _repositorio = repositorio;
    }

    public Task<List<Reserva>> Ejecutar()
    {
        return _repositorio.ListarTodas();
    }
}
