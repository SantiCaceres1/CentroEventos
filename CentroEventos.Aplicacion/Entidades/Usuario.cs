using CentroEventos.Aplicacion.Servicios;

namespace CentroEventos.Aplicacion.Entidades;

public class Usuario
{
    public int Id { get; protected set;}
    public string? _nombre;
    public string? _apellido;
    public string? _correoElectronico;
    public string? _hashContraseña;

    // Constructor
    public Usuario(string nombre, string apellido, string correoElectronico, string hashContraseña)
    {
        _nombre = nombre;
        _apellido = apellido;
        _correoElectronico = correoElectronico;
        _hashContraseña = Hasher.Hashear(hashContraseña);
    }
    
    public Usuario(string nombre,  string correoElectronico, string hashContraseña)
    {
        _nombre = nombre;
        _correoElectronico = correoElectronico;
        _hashContraseña = Hasher.Hashear(hashContraseña);
    }

    public Usuario() { }

    // Relación Muchos a Muchos:
    private List<UsuarioPermiso> _permisos = new();
    
    public string? Nombre => _nombre;
    public string? Apellido => _apellido;
    public string? CorreoElectronico => _correoElectronico;
    public string? HashContraseña => _hashContraseña;
    public List<UsuarioPermiso> Permisos => _permisos;

    public override string ToString()
    {
        return $"[{Id}] {_nombre} {_apellido} - Email: {_correoElectronico} ";
    }

}