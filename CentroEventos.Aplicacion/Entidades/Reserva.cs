
namespace CentroEventos.Aplicacion.Entidades;

public class Reserva
{
    public int Id { get; protected set; }
    public int _idPersona ;
    public int _idEventoDeportivo ;
    public DateTime _fechaAltaReserva ;
    public EstadoAsistencia _estadoAsistencia;

    //constructor sin id
    public Reserva(int idPersona, int idEvento, DateTime fechaAltaReserva)
    {
        _idPersona = idPersona;
        _idEventoDeportivo = idEvento;
        _fechaAltaReserva = fechaAltaReserva;
        _estadoAsistencia = EstadoAsistencia.Pendiente;
    }

    public Reserva() {}

    //Getters publicos
  

    public int IdPersona => _idPersona;

    public int IdEventoDeportivo => _idEventoDeportivo;

    public DateTime FechaAltaReserva => _fechaAltaReserva;

    public EstadoAsistencia EstadoAsistencia => _estadoAsistencia;

    public void AsignarFechaDeAlta(DateTime fecha)
    {
        _fechaAltaReserva = fecha;
    }
    public void AsignarEstadoAsistencia(EstadoAsistencia estado)
    {
        _estadoAsistencia = estado;
    }
    public override string ToString()
    {
        return $"[{Id}]  Persona: {_idPersona} - Evento: {_idEventoDeportivo} - Estado: {_estadoAsistencia}";
    }
}
