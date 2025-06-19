
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

    public ModificarPersonaUseCase(IRepositorioPersona repositorio, IServicioAutorizacion autorizacion)
    {
        _repositorio = repositorio;
        _autorizacion = autorizacion;
        _validador = new ValidadorPersona(repositorio);
    }

    public async Task Ejecutar(Persona persona, int idUsuario)
    {
        var permiso = await _autorizacion.PoseeElPermiso(idUsuario, Permiso.UsuarioModificacion);
        if (!permiso)
            throw new FalloAutorizacionException("El usuario no tiene permiso para modificar personas.");

        if (await _repositorio.ObtenerPorId(persona.Id) == null)
            throw new EntidadNotFoundException("No existe la persona que se quiere modificar.");

        _validador.Validar(persona);

        await _repositorio.Modificar(persona);

    }
}
