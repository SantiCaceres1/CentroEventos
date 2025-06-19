using System.Threading.Tasks;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Repositorios;
using CentroEventos.Aplicacion.Servicios;
using CentroEventos.Aplicacion.Validadores;

namespace CentroEventos.Aplicacion.CasosDeUso.UsuarioU;

public class AltaUsuarioUseCase
{
    private readonly IRepositorioUsuario _repositorioUsuario;
    private readonly IServicioAutorizacion _servicioAutorizacion;
    private readonly ValidadorUsuario _validador;

    public AltaUsuarioUseCase(IRepositorioUsuario repositorioUsuario, IServicioAutorizacion servicioAutorizacion)
    {
        _repositorioUsuario = repositorioUsuario;
        _servicioAutorizacion = servicioAutorizacion;
        _validador = new ValidadorUsuario(repositorioUsuario);
    }

    public async Task Ejecutar(Usuario usuario, int idUsuario)
    {
        var permiso = await _servicioAutorizacion.PoseeElPermiso(idUsuario, Permiso.UsuarioAlta);
        if (!permiso)
            throw new FalloAutorizacionException("El usuario no tiene permiso para dar de alta a usuarios.");
        _validador.Validar(usuario);
        await _repositorioUsuario.Agregar(usuario);
    }
}
