
namespace CentroEventos.Aplicacion.Entidades;

public class Reserva
{
    public int Id { get;  set; }
    public int IdPersona { get;  set; }
    public int IdEventoDeportivo { get;  set; }
    public DateTime FechaAltaReserva { get;  set; }
    public EstadoAsistencia EstadoAsistencia{ get;  set; }

    //constructor sin id
    public Reserva(int idPersona, int idEvento, DateTime fechaAltaReserva)
    {
        IdPersona = idPersona;
        IdEventoDeportivo = idEvento;
        FechaAltaReserva = fechaAltaReserva;
        EstadoAsistencia = EstadoAsistencia.Pendiente;
    }

    public Reserva() {}

    //Getters publicos
  

    public void AsignarFechaDeAlta(DateTime fecha)
    {
        FechaAltaReserva = fecha;
    }
    public void AsignarEstadoAsistencia(EstadoAsistencia estado)
    {
        EstadoAsistencia = estado;
    }
   
    public override string ToString()
    {
        return $"[{Id}]  Persona: {IdPersona} - Evento: {IdEventoDeportivo} - Estado: {EstadoAsistencia}";
    }
}
