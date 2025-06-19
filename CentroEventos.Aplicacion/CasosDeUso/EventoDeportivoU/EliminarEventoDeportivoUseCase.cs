
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Repositorios;
using CentroEventos.Aplicacion.Servicios;

namespace CentroEventos.Aplicacion.CasosDeUso.EventoDeportivoU;

public class EliminarEventoDeportivoUseCase
{
    private readonly IRepositorioEventoDeportivo _repositorioEvento;
    private readonly IRepositorioReserva _repositorioReserva;
    private readonly IServicioAutorizacion _autorizacion;

    public EliminarEventoDeportivoUseCase(IRepositorioEventoDeportivo repositorioEvento, IRepositorioReserva repositorioReserva, IServicioAutorizacion autorizacion)
    {
        _repositorioEvento = repositorioEvento;
        _repositorioReserva = repositorioReserva;
        _autorizacion = autorizacion;
    }

    public async Task Ejecutar(int idEvento, int idUsuario)
    {
        var permiso = await _autorizacion.PoseeElPermiso(idUsuario, Permiso.EventoBaja);
        if (!permiso)
            throw new FalloAutorizacionException("El usuario no tiene permiso para eliminar eventos.");
        if (!await _repositorioEvento.ExisteId(idEvento))
            throw new EntidadNotFoundException("El evento no existe.");
        var reservas = await _repositorioReserva.ListarTodas();
        bool tieneReservas = reservas.Any(r => r.IdEventoDeportivo == idEvento);
        if (tieneReservas)
            throw new OperacionInvalidaException("No se puede eliminar el evento porque tiene reservas asociadas.");
        await _repositorioEvento.Eliminar(idEvento);
    }


}
