using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Repositorios;
using CentroEventos.Aplicacion.Servicios;

namespace CentroEventos.Aplicacion.CasosDeUso.PersonaU;

public class EliminarPersonaUseCase
{
    private readonly IRepositorioPersona _repositorioPersona;
    private readonly IRepositorioEventoDeportivo _repositorioEventos;
    private readonly IRepositorioReserva _repositorioReserva;
    private readonly IServicioAutorizacion _autorizacion;

    public EliminarPersonaUseCase(IRepositorioPersona repositorioPersona, IRepositorioEventoDeportivo repositorioEventos, IRepositorioReserva repositorioReserva, IServicioAutorizacion autorizacion)
    {
        _repositorioPersona = repositorioPersona;
        _repositorioEventos = repositorioEventos;
        _repositorioReserva = repositorioReserva;
        _autorizacion = autorizacion;
    }

    public void Ejecutar(int idPersona, int idUsuario)
    {
        if (!_autorizacion.PoseeElPermiso(idUsuario, Permiso.UsuarioBaja))
            throw new FalloAutorizacionException("No tiene permiso para eliminar personas.");

        if (!_repositorioPersona.ExisteId(idPersona))
            throw new EntidadNotFoundException("La persona no existe.");

        if (_repositorioEventos.ListarTodos().Any(e => e.IdResponsable == idPersona))
            throw new OperacionInvalidaException("No se puede eliminar la persona porque es responsable de un evento.");

        if (_repositorioReserva.ListarTodas().Any(r => r.IdPersona == idPersona))
            throw new OperacionInvalidaException("No se puede eliminar la persona porque tiene reservas asociadas.");

        _repositorioPersona.Eliminar(idPersona);
    }
}
