using System;

namespace CentroEventos.Aplicacion.Entidades;

public class Reserva
{
    private int _id {get; set;}
    private int _idPersona {get; set;}
    private int _idEventoDeportivo {get;set;}
    private DateTime _fechaAltaReserva {get;set;}
    private EstadoAsistencia _estadoAsistencia {get;set;}=EstadoAsistencia.Pendiente;

    public override string ToString()
    {
        return $"[{_id}]  Persona: {_idPersona} - Evento: {_idEventoDeportivo} - Estado: {_estadoAsistencia}";
    }
}
