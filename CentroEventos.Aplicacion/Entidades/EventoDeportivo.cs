
namespace CentroEventos.Aplicacion.Entidades;

public class EventoDeportivo
{
    public int Id { get;  set; }
    public string? Nombre{ get;  set; }
    public string? Descripcion{ get;  set; }
    public DateTime FechaInicio { get;  set; }
    public double DuracionHoras { get;  set; }
    public int CupoMaximo { get;  set; }
    public int IdResponsable { get;  set; }

    public EventoDeportivo() { }

    //Constructor con id
    public EventoDeportivo(int id,string nombre, string descripcion, DateTime fechaHoraInicio, double duracion, int cupoMax, int idResponsable)
    {
        Id = id;
        Nombre = nombre;
        Descripcion = descripcion;
        FechaInicio = fechaHoraInicio;
        DuracionHoras = duracion;
        CupoMaximo = cupoMax;
        IdResponsable = idResponsable;
    }

   
    public override string ToString()
    {
        return $"[{Id}] {Nombre} {FechaInicio} - {DuracionHoras}HS - Cupo: {CupoMaximo}";
    }

}
