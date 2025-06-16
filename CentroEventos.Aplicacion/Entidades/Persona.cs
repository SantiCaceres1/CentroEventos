
namespace CentroEventos.Aplicacion.Entidades;

public class Persona
{
    private int _id;
    private string? _nombre;
    private string ? _apellido;
    private string ? _Dni;
    private string ? _email;
    private string _telefono;

    //Constructor sin ID
    public Persona(string dni, string nombre, string apellido, string email, string telefono)
    {
        _Dni = dni;
        _nombre = nombre;
        _apellido = apellido;
        _email = email;
        _telefono = telefono;
    }

    /*
        //Constructor con ID
        public Persona(int id,string dni,string nombre,string apellido,string email, string telefono)
        {
            _id = id;
            _Dni = dni;
            _nombre = nombre;
            _apellido = apellido;
            _email = email;
            _telefono = telefono;
        }
    */

    //Getters publicos
    public int Id => _id;
    public string? Dni => _Dni;
    public string? Nombre => _nombre;
    public string? Apellido => _apellido;
    public string? Email => _email;
    public string Telefono => _telefono;

    public override string ToString()
    {
        return $"[{_id}]{_nombre} {_apellido} - DNI: {_Dni} - Email: {_email} ";
    }
}
