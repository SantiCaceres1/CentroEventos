using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Repositorios;
using CentroEventos.Aplicacion.Servicios;
using CentroEventos.Aplicacion.Validadores;

namespace CentroEventos.Aplicacion.CasosDeUso.PersonaU;

public class AltaPersonaUseCase
{
    private readonly IRepositorioPersona _repositorioPersona;
    private readonly IServicioAutorizacion _servicioAutorizacion;
    private readonly ValidadorPersona _validador;

    public AltaPersonaUseCase(
        IRepositorioPersona repositorioPersona,
        IServicioAutorizacion servicioAutorizacion,
        ValidadorPersona validador)
    {
        _repositorioPersona = repositorioPersona;
        _servicioAutorizacion = servicioAutorizacion;
        _validador = validador;
    }

    public async Task Ejecutar(Persona persona, int idUsuario)
    {
        var permiso = await _servicioAutorizacion.PoseeElPermiso(idUsuario, Permiso.UsuarioAlta);
        if (!permiso)
            throw new FalloAutorizacionException("El usuario no tiene permiso para dar de alta personas.");

        var errores = await _validador.Validar(persona);
        if (errores.Any())
            throw new ValidacionException(errores);

        await _repositorioPersona.Agregar(persona);
    }
}