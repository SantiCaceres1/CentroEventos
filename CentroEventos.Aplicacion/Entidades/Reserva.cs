using System;

namespace CentroEventos.Aplicacion.Entidades;

public class Reserva
{
    private int _id ;
    private int _idPersona ;
    private int _idEventoDeportivo ;
    private DateTime _fechaAltaReserva ;
    private EstadoAsistencia _estadoAsistencia;

    public int Id => _id;

    public int IdPersona => _idPersona;

    public int IdEventoDeportivo => _idEventoDeportivo;

    public DateTime FechaAltaReserva => _fechaAltaReserva;

    public EstadoAsistencia EStadoAsistencia => _estadoAsistencia;

    public override string ToString()
    {
        return $"[{_id}]  Persona: {_idPersona} - Evento: {_idEventoDeportivo} - Estado: {_estadoAsistencia}";
    }
}
