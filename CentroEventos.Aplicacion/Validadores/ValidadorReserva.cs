using System;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Repositorios;

namespace CentroEventos.Aplicacion.Validadores;

public class ValidadorReserva
{
   private readonly IRepositorioReserva _repoReserva;

    private readonly IRepositorioPersona _repoPersona;

    private readonly IRepositorioEventoDeportivo _repoEvento;

    public ValidadorReserva(IRepositorioReserva repoReserva,IRepositorioPersona repoPersona,IRepositorioEventoDeportivo repoEvento)
    {
        _repoReserva = repoReserva;
        
        _repoPersona = repoPersona;

        _repoEvento = repoEvento;
    }

    public void Validar(Reserva reserva)
    {
        if(!_repoPersona.ExisteId(reserva.IdPersona))
            throw new EntidadNotFoundException("No existe la persona indicada.");

        if(!_repoEvento.ExisteId(reserva.IdEventoDeportivo))
            throw new EntidadNotFoundException("No existe el evento indicado.");

        if(_repoReserva.ExisteResercaDuplicada(reserva.IdPersona,reserva.IdEventoDeportivo))
            throw new DuplicadoException("La persona ya tiene una reserva para ese evento.");

        var cupoDisponible = _repoEvento.HayCupoDisponible(reserva.IdEventoDeportivo);
        if(!cupoDisponible)
            throw new CupoExcedidoException("No hay cupo disponible para ese evento");
    }

}
