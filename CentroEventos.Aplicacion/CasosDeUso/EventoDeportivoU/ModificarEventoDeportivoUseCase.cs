using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Repositorios;
using CentroEventos.Aplicacion.Servicios;
using CentroEventos.Aplicacion.Validadores;

public class ModificarEventoDeportivoUseCase
{
    private IRepositorioEventoDeportivo _repositorioEvento;
    private IServicioAutorizacion _autorizacion;
    private ValidadorEventoDeportivo _validador;

    public ModificarEventoDeportivoUseCase(
        IRepositorioEventoDeportivo repositorioEvento,
        IServicioAutorizacion autorizacion,
        ValidadorEventoDeportivo validador)
    {
        _repositorioEvento = repositorioEvento;
        _autorizacion = autorizacion;
        _validador = validador;
    }

    public void Ejecutar(EventoDeportivo eventoModificado, int idUsuario)
    {
        var permiso = _autorizacion.PoseeElPermiso(idUsuario, Permiso.EventoModificacion);
        if (!permiso)
            throw new FalloAutorizacionException("El usuario no tiene permiso para modificar eventos.");
        var eventoExistente = _repositorioEvento.ObtenerPorId(eventoModificado.Id) ??
            throw new EntidadNotFoundException("El evento a modificar no existe.");
        if (eventoExistente.FechaInicio < DateTime.Now)
            throw new OperacionInvalidaException("No se puede modificar un evento que ya ocurrió.");
        var errores = _validador.Validar(eventoModificado);
        if (errores.Any())
            throw new ValidacionException(errores);
        _repositorioEvento.Modificar(eventoModificado);
    }
}