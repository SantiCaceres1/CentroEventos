
namespace CentroEventos.Aplicacion.Entidades;

public class Persona
{
    public int Id { get; protected set; }
    private string? _nombre;
    private string ? _apellido;
    private string ? _Dni;
    private string ? _email;
    private string ?  _telefono;

    //Constructor sin ID
    public Persona(string dni, string nombre, string apellido, string email, string telefono)
    {
        _Dni = dni;
        _nombre = nombre;
        _apellido = apellido;
        _email = email;
        _telefono = telefono;
    }

    public Persona() { }

    //Getters publicos
 
    public string? Dni => _Dni;
    public string? Nombre => _nombre;
    public string? Apellido => _apellido;
    public string? Email => _email;
    public string? Telefono => _telefono;

    public override string ToString()
    {
        return $"[{Id}]{_nombre} {_apellido} - DNI: {_Dni} - Email: {_email} ";
    }
}
