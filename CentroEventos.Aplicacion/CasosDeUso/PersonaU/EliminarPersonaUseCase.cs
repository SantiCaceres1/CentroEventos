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

    public async void Ejecutar(int idPersona, int idUsuario)
    {
        var permiso = await _autorizacion.PoseeElPermiso(idUsuario, Permiso.UsuarioBaja);
        if (!permiso)
            throw new FalloAutorizacionException("No tiene permiso para eliminar personas.");

        if (_repositorioPersona.ObtenerPorId(idPersona) == null)
            throw new EntidadNotFoundException("La persona no existe.");

        var eventos = await _repositorioEventos.ListarTodos();
        if (eventos.Any(e => e.IdResponsable == idPersona))
            throw new OperacionInvalidaException("No se puede eliminar la persona porque es responsable de un evento.");

        if (eventos.Any(r => r.Id == idPersona))
            throw new OperacionInvalidaException("No se puede eliminar la persona porque tiene reservas asociadas.");

        await _repositorioPersona.Eliminar(idPersona);
    }
}
