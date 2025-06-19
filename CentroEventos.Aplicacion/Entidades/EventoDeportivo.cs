
namespace CentroEventos.Aplicacion.Entidades;

public class EventoDeportivo
{
    private int _id;

    private string? _nombre;

    private string? _descripcion;

    private DateTime _fechaInicio ;

    private double _duracionHoras ;

    private int _cupoMaximo ;

    private int _idResponsable ;

    //Constructor sin id
    // public EventoDeportivo(string nombre, string descripcion, DateTime fechaHoraInicio, double duracion, int cupoMax, int idResponsable)
    // {
    //     _nombre = nombre;
    //     _descripcion = descripcion;
    //     _fechaInicio = fechaHoraInicio;
    //     _duracionHoras = duracion;
    //     _cupoMaximo = cupoMax;
    //     _idResponsable = idResponsable;
    // }
    //Constructor con id
    public EventoDeportivo(int id,string nombre, string descripcion, DateTime fechaHoraInicio, double duracion, int cupoMax, int idResponsable)
    {
        _id = id;
        _nombre = nombre;
        _descripcion = descripcion;
        _fechaInicio = fechaHoraInicio;
        _duracionHoras = duracion;
        _cupoMaximo = cupoMax;
        _idResponsable = idResponsable;
    }

    //Getters publicos
    public int Id => _id;

    public string? Nombre => _nombre;

    public string? Descripcion => _descripcion;

    public DateTime  FechaInicio => _fechaInicio;

    public double DuracionHoras => _duracionHoras;

    public int CupoMaximo => _cupoMaximo;

    public int IdResponsable => _idResponsable;

    public void AsignarId(int id)
    {
        _id = id;
    }

    public override string ToString()
    {
        return $"[{_id}] {_nombre} {_fechaInicio} - {_duracionHoras}HS - Cupo: {_cupoMaximo}";
    }

}
