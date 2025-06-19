
using System.Threading.Tasks;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Repositorios;
using CentroEventos.Aplicacion.Servicios;

namespace CentroEventos.Aplicacion.CasosDeUso.ReservaU;

public class EliminarReservaUseCase
{
    private readonly IRepositorioReserva _repositorio;
    private readonly IServicioAutorizacion _autorizacion;

    public EliminarReservaUseCase(IRepositorioReserva repositorio, IServicioAutorizacion autorizacion)
    {
        _repositorio = repositorio;
        _autorizacion = autorizacion;
    }

    public async Task Ejecutar(int idReserva, int idUsuario)
    {
        if (!_autorizacion.PoseeElPermiso(idUsuario, Entidades.Permiso.ReservaBaja))
            throw new FalloAutorizacionException("No tiene permiso para eliminar reservas");
        if (!await _repositorio.ExisteReserva(idReserva))
            throw new EntidadNotFoundException("La reserva no existe.");
        await _repositorio.Eliminar(idReserva);
    }
}
