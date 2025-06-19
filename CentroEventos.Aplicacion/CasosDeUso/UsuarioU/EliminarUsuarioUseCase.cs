using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Repositorios;
using CentroEventos.Aplicacion.Servicios;

namespace CentroEventos.Aplicacion.CasosDeUso.UsuarioU;

public class EliminarUsuarioUseCase
{
    private readonly IRepositorioUsuario _repositorioUsuario;
    private readonly IRepositorioEventoDeportivo _repositorioEventos;
    private readonly IRepositorioReserva _repositorioReserva;
    private readonly IServicioAutorizacion _autorizacion;

    public EliminarUsuarioUseCase(IRepositorioUsuario repositorioUsuario, IRepositorioEventoDeportivo repositorioEventos, IRepositorioReserva repositorioReserva, IServicioAutorizacion autorizacion)
    {
        _repositorioUsuario = repositorioUsuario;
        _repositorioEventos = repositorioEventos;
        _repositorioReserva = repositorioReserva;
        _autorizacion = autorizacion;
    }

    public async Task Ejecutar(int idUsuario, int idAdmin)
    {
        var permiso = await _autorizacion.PoseeElPermiso(idAdmin, Permiso.UsuarioBaja);
        if (!permiso)
            throw new FalloAutorizacionException("No tiene permiso para eliminar personas.");

        if (!await _repositorioUsuario.ExisteID(idUsuario))
            throw new EntidadNotFoundException("El usuario no existe.");

        var eventos = await _repositorioEventos.ListarTodos();
        if (eventos.Any(e => e.IdResponsable == idUsuario))
            throw new OperacionInvalidaException("No se puede eliminar el usuario porque es responsable de un evento.");

        var reservas = await _repositorioReserva.ListarTodas(); 
        if (reservas.Any(r => r.IdPersona == idUsuario))
            throw new OperacionInvalidaException("No se puede eliminar el usuario porque tiene reservas asociadas.");

        var usuario = await _repositorioUsuario.ObtenerPorId(idUsuario);
        await _repositorioUsuario.Eliminar(usuario);
    }
}
