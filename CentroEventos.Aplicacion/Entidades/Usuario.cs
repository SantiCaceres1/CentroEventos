using CentroEventos.Aplicacion.Servicios;

namespace CentroEventos.Aplicacion.Entidades;

    public class Usuario
    {
        private string ? _nombre { get; set; }
        private string ? _apellido { get; set; }
        private string ? _correoElectronico { get; set; }
        private string ? _hashContraseña { get; set; }

    // Constructor
    public Usuario(string nombre, string apellido, string correoElectronico, string hashContraseña)
    {
        _nombre = nombre;
        _apellido = apellido;
        _correoElectronico = correoElectronico;
        _hashContraseña = Hasher.Hashear(hashContraseña);
    }

        // Relación Muchos a Muchos:
        private List<UsuarioPermiso> Permisos { get; set; } = new();
}