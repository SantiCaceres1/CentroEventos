
namespace CentroEventos.Aplicacion.Entidades;

public class EventoDeportivo
{
    public int Id { get; protected set; }

    public string? _nombre;

    public string? _descripcion;

    public DateTime _fechaInicio ;

    public double _duracionHoras ;

    public int _cupoMaximo ;

    public int _idResponsable ;

    public EventoDeportivo() { }

    //Constructor con id
    public EventoDeportivo(int id,string nombre, string descripcion, DateTime fechaHoraInicio, double duracion, int cupoMax, int idResponsable)
    {
        Id = id;
        _nombre = nombre;
        _descripcion = descripcion;
        _fechaInicio = fechaHoraInicio;
        _duracionHoras = duracion;
        _cupoMaximo = cupoMax;
        _idResponsable = idResponsable;
    }

    //Getters publicos
  

    public string? Nombre => _nombre;

    public string? Descripcion => _descripcion;

    public DateTime  FechaInicio => _fechaInicio;

    public double DuracionHoras => _duracionHoras;

    public int CupoMaximo => _cupoMaximo;

    public int IdResponsable => _idResponsable;

  
    public override string ToString()
    {
        return $"[{Id}] {_nombre} {_fechaInicio} - {_duracionHoras}HS - Cupo: {_cupoMaximo}";
    }

}
