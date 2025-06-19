
using System.Threading.Tasks;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Repositorios;
using CentroEventos.Aplicacion.Servicios;

namespace CentroEventos.Aplicacion.CasosDeUso.ReservaU;

public class ModificarReservaUseCase
{
    private readonly IRepositorioReserva _repositorio;
    private readonly IServicioAutorizacion _autorizacion;

    public ModificarReservaUseCase(IRepositorioReserva repositorio, IServicioAutorizacion autorizacion)
    {
        _repositorio = repositorio;
        _autorizacion = autorizacion;
    }

    public async Task Ejecutar(Reserva reserva, int idUsuario)
    {
        var permiso = await _autorizacion.PoseeElPermiso(idUsuario, Permiso.ReservaModificacion);
        if (!permiso)
            throw new FalloAutorizacionException("No tiene permiso para modifcar reservas.");
            
        if (!await _repositorio.ExisteReserva(reserva.Id))
            throw new EntidadNotFoundException("La reserva no existe.");
        await _repositorio.Modificar(reserva);
    }




}
