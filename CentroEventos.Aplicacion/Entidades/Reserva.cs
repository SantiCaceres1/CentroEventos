
namespace CentroEventos.Aplicacion.Entidades;

public class Reserva
{
    private int _id ;
    private int _idPersona ;
    private int _idEventoDeportivo ;
    private DateTime _fechaAltaReserva ;
    private EstadoAsistencia _estadoAsistencia;

    //constructor sin id
    public Reserva(int idPersona, int idEvento, DateTime fechaAltaReserva)
    {
        _idPersona = idPersona;
        _idEventoDeportivo = idEvento;
        _fechaAltaReserva = fechaAltaReserva;
        _estadoAsistencia = EstadoAsistencia.Pendiente;
    }

    //constructor con id
    // public Reserva(int id, int idPersona, int idEvento, DateTime fechaAltaReserva)
    // {
    //     _id = id;
    //     _idPersona = idPersona;
    //     _idEventoDeportivo = idEvento;
    //     _fechaAltaReserva = fechaAltaReserva;
    //     _estadoAsistencia = EstadoAsistencia.Pendiente;
    // }
    //Getters publicos
    public int Id => _id;

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
    public void AsignarId(int id)
    {
        _id = id;
    }
    public override string ToString()
    {
        return $"[{_id}]  Persona: {_idPersona} - Evento: {_idEventoDeportivo} - Estado: {_estadoAsistencia}";
    }
}
