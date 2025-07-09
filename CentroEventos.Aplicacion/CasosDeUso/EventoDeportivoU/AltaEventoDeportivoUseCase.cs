using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Repositorios;
using CentroEventos.Aplicacion.Servicios;
using CentroEventos.Aplicacion.Validadores;

public class AltaEventoDeportivoUseCase
{
    private IRepositorioEventoDeportivo _repositorioEvento;
    private IServicioAutorizacion _autorizacion;
    private ValidadorEventoDeportivo _validador;

    public AltaEventoDeportivoUseCase(
        IRepositorioEventoDeportivo repositorioEvento,
        IServicioAutorizacion autorizacion,
        ValidadorEventoDeportivo validador)
    {
        _repositorioEvento = repositorioEvento;
        _autorizacion = autorizacion;
        _validador = validador;
    }

    public void Ejecutar(EventoDeportivo eventoDeportivo, int idUsuario)
    {
        var permiso = _autorizacion.PoseeElPermiso(idUsuario, Permiso.EventoAlta);
        if (!permiso)
            throw new FalloAutorizacionException("El usuario no tiene permiso para dar de alta eventos.");
        var errores = _validador.Validar(eventoDeportivo);
        if (errores.Any())
            throw new ValidacionException(errores);
        _repositorioEvento.Agregar(eventoDeportivo);
    }
}