using CentroEventos.Aplicacion.Entidades;
public class UsuarioPermiso
{
    // Clave primaria
    public int PK { get; set; }
    
    // Clave foránea a Usuario
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;
    public Permiso Permiso { get; set; }
}