using System.Threading.Tasks;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Repositorios;
using CentroEventos.Aplicacion.Servicios;
using CentroEventos.Aplicacion.Validadores;

namespace CentroEventos.Aplicacion.CasosDeUso.PersonaU;

public class ModificarPersonaUseCase
{
    private readonly IRepositorioPersona _repositorio;
    private readonly IServicioAutorizacion _autorizacion;
    private readonly ValidadorPersona _validador;

    public ModificarPersonaUseCase(
        IRepositorioPersona repositorio,
        IServicioAutorizacion autorizacion,
        ValidadorPersona validador)
    {
        _repositorio = repositorio;
        _autorizacion = autorizacion;
        _validador = validador;
    }

    public async Task Ejecutar(Persona persona, int idUsuario)
    {
        var permiso = await _autorizacion.PoseeElPermiso(idUsuario, Permiso.UsuarioModificacion);
        if (!permiso)
            throw new FalloAutorizacionException("El usuario no tiene permiso para modificar personas.");

        var personaExistente = await _repositorio.ObtenerPorId(persona.Id);
        if (personaExistente == null)
            throw new EntidadNotFoundException("No existe la persona que se quiere modificar.");

        var errores = await _validador.Validar(persona);
        if (errores.Any())
            throw new ValidacionException(errores);

        await _repositorio.Modificar(persona);
    }
}