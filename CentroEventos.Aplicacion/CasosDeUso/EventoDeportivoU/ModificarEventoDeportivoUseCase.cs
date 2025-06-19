using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Repositorios;
using CentroEventos.Aplicacion.Servicios;
using CentroEventos.Aplicacion.Validadores;

namespace CentroEventos.Aplicacion.CasosDeUso.EventoDeportivoU;

public class ModificarEventoDeportivoUseCase
{
    private readonly IRepositorioEventoDeportivo _repositorioEvento;
    private readonly IRepositorioPersona _repositorioPersona;
    private readonly IServicioAutorizacion _autorizacion;
    private readonly ValidadorEventoDeportivo _validador;

    public ModificarEventoDeportivoUseCase(IRepositorioEventoDeportivo repositorioEvento, IRepositorioPersona repositorioPersona, IServicioAutorizacion autorizacion)
    {
        _repositorioEvento = repositorioEvento;
        _repositorioPersona = repositorioPersona;
        _autorizacion = autorizacion;
        _validador = new ValidadorEventoDeportivo(repositorioEvento, repositorioPersona);
    }

    public async Task Ejecutar(EventoDeportivo eventoModificado, int idUsuario)
    {
        if (!_autorizacion.PoseeElPermiso(idUsuario, Permiso.EventoModificacion))
            throw new FalloAutorizacionException("El usuario no tiene el permiso para modificar eventos.");
        var eventoExistente = await _repositorioEvento.ObtenerPorId(eventoModificado.Id);
        if (eventoExistente == null)
            throw new EntidadNotFoundException("El evento a modificar no existe.");
        if (eventoExistente.FechaInicio < DateTime.Now)
            throw new OperacionInvalidaException("No se puede modificar un evento que ya ocurrio.");
        _validador.Validar(eventoModificado);
        await _repositorioEvento.Modificar(eventoModificado);
    }
}
