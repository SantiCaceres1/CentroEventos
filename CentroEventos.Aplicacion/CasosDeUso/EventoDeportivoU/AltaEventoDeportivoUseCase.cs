using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Repositorios;
using CentroEventos.Aplicacion.Servicios;
using CentroEventos.Aplicacion.Validadores;

namespace CentroEventos.Aplicacion.CasosDeUso.EventoDeportivoU;

public class AltaEventoDeportivoUseCase
{
    private readonly IRepositorioEventoDeportivo _repositorioEvento;
    private readonly IRepositorioPersona _repositorioPersona;
    private readonly IServicioAutorizacion _autorizacion;
    private readonly ValidadorEventoDeportivo _validador;

    public AltaEventoDeportivoUseCase(IRepositorioEventoDeportivo repositorioEvento, IRepositorioPersona repositorioPersona, IServicioAutorizacion autorizacion)
    {
        _repositorioEvento = repositorioEvento;
        _repositorioPersona = repositorioPersona;
        _autorizacion = autorizacion;
        _validador = new ValidadorEventoDeportivo(repositorioEvento, repositorioPersona);
    }

    public void Ejecutar(EventoDeportivo eventoDeportivo, int idUsuario)
    {
        if (!_autorizacion.PoseeElPermiso(idUsuario, Permiso.EventoAlta))
            throw new FalloAutorizacionException("El usuario no tiene permiso para dar de alta eventos.");

        
        _validador.Validar(eventoDeportivo);
        _repositorioEvento.Agregar(eventoDeportivo);
    }
}
