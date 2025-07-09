using System.Threading.Tasks;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Repositorios;
using CentroEventos.Aplicacion.Servicios;
using CentroEventos.Aplicacion.Validadores;

namespace CentroEventos.Aplicacion.CasosDeUso.PersonaU;

public class ModificarPersonaUseCase
{
    private IRepositorioPersona _repositorio;
    private IServicioAutorizacion _autorizacion;
    private ValidadorPersona _validador;

    public ModificarPersonaUseCase(
        IRepositorioPersona repositorio,
        IServicioAutorizacion autorizacion,
        ValidadorPersona validador)
    {
        _repositorio = repositorio;
        _autorizacion = autorizacion;
        _validador = validador;
    }

    public void Ejecutar(Persona persona, int idUsuario)
    {
        var permiso = _autorizacion.PoseeElPermiso(idUsuario, Permiso.UsuarioModificacion);
        if (!permiso)
            throw new FalloAutorizacionException("El usuario no tiene permiso para modificar personas.");
        var personaExistente = _repositorio.ObtenerPorId(persona.Id);
        if (personaExistente == null)
            throw new EntidadNotFoundException("No existe la persona que se quiere modificar.");
        var errores = _validador.Validar(persona);
        if (errores.Any())
            throw new ValidacionException(errores);
        _repositorio.Modificar(persona);
    }
}