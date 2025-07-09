using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Repositorios;
using CentroEventos.Aplicacion.Servicios;
using CentroEventos.Aplicacion.Validadores;

public class ModificarEventoDeportivoUseCase
{
    private readonly IRepositorioEventoDeportivo _repositorioEvento;
    private readonly IServicioAutorizacion _autorizacion;
    private readonly ValidadorEventoDeportivo _validador;

    public ModificarEventoDeportivoUseCase(
        IRepositorioEventoDeportivo repositorioEvento,
        IServicioAutorizacion autorizacion,
        ValidadorEventoDeportivo validador)
    {
        _repositorioEvento = repositorioEvento;
        _autorizacion = autorizacion;
        _validador = validador;
    }

    public async Task Ejecutar(EventoDeportivo eventoModificado, int idUsuario)
    {
        var permiso = await _autorizacion.PoseeElPermiso(idUsuario, Permiso.EventoModificacion);
        if (!permiso)
            throw new FalloAutorizacionException("El usuario no tiene permiso para modificar eventos.");

        var eventoExistente = await _repositorioEvento.ObtenerPorId(eventoModificado.Id) ??
            throw new EntidadNotFoundException("El evento a modificar no existe.");

        if (eventoExistente.FechaInicio < DateTime.Now)
            throw new OperacionInvalidaException("No se puede modificar un evento que ya ocurrió.");

        var errores = await _validador.Validar(eventoModificado);
        if (errores.Any())
            throw new ValidacionException(errores);

        await _repositorioEvento.Modificar(eventoModificado);
    }
}