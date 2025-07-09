using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Repositorios;
using CentroEventos.Aplicacion.Servicios;

namespace CentroEventos.Aplicacion.CasosDeUso.UsuarioU;

public class EliminarUsuarioUseCase
{
    private IRepositorioUsuario _repositorioUsuario;
    private IRepositorioEventoDeportivo _repositorioEventos;
    private IRepositorioReserva _repositorioReserva;
    private IServicioAutorizacion _autorizacion;

    public EliminarUsuarioUseCase(IRepositorioUsuario repositorioUsuario, IRepositorioEventoDeportivo repositorioEventos, IRepositorioReserva repositorioReserva, IServicioAutorizacion autorizacion)
    {
        _repositorioUsuario = repositorioUsuario;
        _repositorioEventos = repositorioEventos;
        _repositorioReserva = repositorioReserva;
        _autorizacion = autorizacion;
    }

    public void Ejecutar(int idUsuario, int idAdmin)
    {
        var permiso = _autorizacion.PoseeElPermiso(idAdmin, Permiso.UsuarioBaja);
        if (!permiso)
            throw new FalloAutorizacionException("No tiene permiso para eliminar personas.");
        if (!_repositorioUsuario.ExisteID(idUsuario))
            throw new EntidadNotFoundException("El usuario no existe.");
        var eventos = _repositorioEventos.ListarTodos();
        if (eventos.Any(e => e.IdResponsable == idUsuario))
            throw new OperacionInvalidaException("No se puede eliminar el usuario porque es responsable de un evento.");
        var reservas = _repositorioReserva.ListarTodas();
        if (reservas.Any(r => r.IdPersona == idUsuario))
            throw new OperacionInvalidaException("No se puede eliminar el usuario porque tiene reservas asociadas.");
        var usuario = _repositorioUsuario.ObtenerPorId(idUsuario);
        _repositorioUsuario.Eliminar(usuario);
    }
}
