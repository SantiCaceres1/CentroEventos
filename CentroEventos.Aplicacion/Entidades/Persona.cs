
namespace CentroEventos.Aplicacion.Entidades;

public class Persona
{
    public int Id { get;  set; }
    public string? Nombre{ get;  set; }
    public string ? Apellido{ get;  set; }
    public string ? Dni{ get;  set; }
    public string ? Email{ get;  set; }
    public string ?  Telefono{ get;  set; }

    //Constructor sin ID
    public Persona(string dni, string nombre, string apellido, string email, string telefono)
    {
        Dni = dni;
        Nombre = nombre;
        Apellido = apellido;
        Email = email;
        Telefono = telefono;
    }

    public Persona() { }

    public override string ToString()
    {
        return $"[{Id}]{Nombre} {Apellido} - DNI: {Dni} - Email: {Email} ";
    }
}
