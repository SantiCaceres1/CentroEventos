
namespace CentroEventos.Aplicacion.Entidades;

public class Persona
{
    private int _id;
    private string ? _nombre;
    private string ? _apellido ;
    private string ? _Dni ;

    private string?  _email ;

    private long _telefono ;

    public int Id => _id;
    public string?  Dni=> _Dni;
    public string? Nombre => _nombre;
    public string? Apellido => _apellido;
    public string? Email => _email;
    public long Telefono => _telefono;

    public override string ToString()
    {
        return $"[{_id}] {_nombre} {_apellido} - DNI: {_Dni} - Email: {_email} ";
    }
}
