using CentroEventos.Aplicacion.Servicios;

namespace CentroEventos.Aplicacion.Entidades;

public class Usuario
{
    private int _id;
    private string? _nombre;
    private string? _apellido;
    private string? _correoElectronico;
    private string? _hashContraseña;

    // Constructor
    public Usuario(string nombre, string apellido, string correoElectronico, string hashContraseña)
    {
        _nombre = nombre;
        _apellido = apellido;
        _correoElectronico = correoElectronico;
        _hashContraseña = Hasher.Hashear(hashContraseña);
    }

    // Relación Muchos a Muchos:
    private List<UsuarioPermiso> _permisos = new();
    public int ID => _id;
    public string? Nombre => _nombre;
    public string? Apellido => _apellido;
    public string? CorreoElectronico => _correoElectronico;
    public string? HashContraseña => _hashContraseña;
    public List<UsuarioPermiso> Permisos => _permisos;

    public override string ToString()
    {
        return $"[{ID}] {Nombre} {Apellido} - Email: {CorreoElectronico} ";
    }

    public bool VerificarContraseña(string contraseña)
    {
        return Hasher.Verificar(contraseña, _hashContraseña!);
    }

}