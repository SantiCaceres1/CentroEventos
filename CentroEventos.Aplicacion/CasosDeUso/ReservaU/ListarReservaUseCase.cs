
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Repositorios;

namespace CentroEventos.Aplicacion.CasosDeUso.ReservaU;

public class ListarReservaUseCase
{
    private IRepositorioReserva _repositorio;

    public ListarReservaUseCase(IRepositorioReserva repositorio)
    {
        _repositorio = repositorio;
    }

    public List<Reserva> Ejecutar()
    {
        return _repositorio.ListarTodas();
    }
}
