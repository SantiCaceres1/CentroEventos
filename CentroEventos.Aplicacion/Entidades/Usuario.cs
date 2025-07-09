using CentroEventos.Aplicacion.Servicios;

namespace CentroEventos.Aplicacion.Entidades;

public class Usuario
{
    public int Id { get;  set; }
    public string? Nombre{ get;  set; }
    public string? Apellido{ get;  set; }
    public string? CorreoElectronico{ get;  set; }
    public string? HashContraseña{ get;  set; }
    public List<UsuarioPermiso> Permisos{ get;  set; } = new();

    // Constructor
    public Usuario() { }
    public Usuario(string nombre, string apellido, string correoElectronico, string hashContraseña)
    {
        Nombre = nombre;
        Apellido = apellido;
        CorreoElectronico = correoElectronico;
        HashContraseña = Hasher.Hashear(hashContraseña);
    }
    
    public Usuario(string nombre,  string correoElectronico, string hashContraseña)
    {
        Nombre = nombre;
        CorreoElectronico = correoElectronico;
        HashContraseña = Hasher.Hashear(hashContraseña);
    }



   
    public override string ToString()
    {
        return $"[{Id}] {Nombre} {Apellido} - Email: {CorreoElectronico} ";
    }

}