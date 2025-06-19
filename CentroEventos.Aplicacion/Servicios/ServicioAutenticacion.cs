namespace CentroEventos.Aplicacion.Servicios;

public class ServicioAutenticacion
{
    public bool VerificarContraseña(string contraseña, string _hashContraseña)
    {
        return Hasher.Verificar(contraseña, _hashContraseña!);
    }
}