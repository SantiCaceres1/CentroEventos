using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Repositorios;
using CentroEventos.Aplicacion.Servicios;
using CentroEventos.Aplicacion.Validadores;

namespace CentroEventos.Aplicacion.CasosDeUso.PersonaU;

public class AltaPersonaUseCase
{
    private IRepositorioPersona _repositorioPersona;
    private IServicioAutorizacion _servicioAutorizacion;
    private ValidadorPersona _validador;

    public AltaPersonaUseCase(
        IRepositorioPersona repositorioPersona,
        IServicioAutorizacion servicioAutorizacion,
        ValidadorPersona validador)
    {
        _repositorioPersona = repositorioPersona;
        _servicioAutorizacion = servicioAutorizacion;
        _validador = validador;
    }

    public void Ejecutar(Persona persona, int idUsuario)
    {
        var permiso = _servicioAutorizacion.PoseeElPermiso(idUsuario, Permiso.UsuarioAlta);
        if (!permiso)
            throw new FalloAutorizacionException("El usuario no tiene permiso para dar de alta personas.");
        var errores = _validador.Validar(persona);
        if (errores.Any())
            throw new ValidacionException(errores);
        _repositorioPersona.Agregar(persona);
    }
}