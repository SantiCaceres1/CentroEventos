
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Repositorios;
using CentroEventos.Aplicacion.Servicios;
using CentroEventos.Aplicacion.Validadores;

namespace CentroEventos.Aplicacion.CasosDeUso.UsuarioU;

public class ModificarUsuarioUseCase
{
    private readonly IRepositorioUsuario _repositorio;
    private readonly IServicioAutorizacion _autorizacion;
    private readonly ValidadorUsuario _validador;

    public ModificarUsuarioUseCase(IRepositorioUsuario repositorio, IServicioAutorizacion autorizacion)
    {
        _repositorio = repositorio;
        _autorizacion = autorizacion;
        _validador = new ValidadorUsuario(repositorio);
    }

    public async Task Ejecutar(Usuario usuario, int idAdmin)
    {
        var permiso = await _autorizacion.PoseeElPermiso(idAdmin, Permiso.UsuarioModificacion);
        if (!permiso)
            throw new FalloAutorizacionException("El usuario no tiene permiso para modificar personas.");

        if (!await _repositorio.ExisteID(usuario.ID))
            throw new EntidadNotFoundException("No existe el usuario que se quiere modificar.");

        _validador.Validar(usuario);

        await _repositorio.Modificar(usuario);

    }
}
