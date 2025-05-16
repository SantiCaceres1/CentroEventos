using System;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Repositorios;

namespace CentroEventos.Aplicacion.Validadores;

public class ValidadorEventoDeportivo
{
    private readonly IRepositorioEventoDeportivo _repoEvento;
    private readonly IRepositorioPersona _repoPersona;

    public ValidadorEventoDeportivo(IRepositorioEventoDeportivo repoEvento, IRepositorioPersona repoPersona)
    {
        _repoEvento = repoEvento;
        _repoPersona = repoPersona;
    }

    public void Validar(EventoDeportivo evento)
    {
        if(string.IsNullOrWhiteSpace(evento.Nombre))
            throw new ValidacionException("El nombre del evento no puede estar vacio.");

        if(string.IsNullOrWhiteSpace(evento.Descripcion))
            throw new ValidacionException("La descripcion del evento no puede estar vacia.");
        
        if(evento.CupoMaximo <=0)
            throw new ValidacionException("El cupo maximo debe ser mayor que 0");

        if(evento.DuracionHoras<=0)
            throw new ValidacionException("La duracion debe ser mayor que cero.");

        if(evento.FechaInicio < DateTime.Now)
            throw new ValidacionException("La fecha del evento debe ser futura o actual");

        if(! _repoPersona.ExisteId(evento.IdResponsable))
            throw new EntidadNotFoundException("No existe la persona responsable indicada");
        
    }
}
