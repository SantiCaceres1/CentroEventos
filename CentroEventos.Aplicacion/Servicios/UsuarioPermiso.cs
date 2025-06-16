using CentroEventos.Aplicacion.Entidades;
public class UsuarioPermiso
{
    // Clave foránea a Usuario
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;
    public Permiso Permiso { get; set; }
}