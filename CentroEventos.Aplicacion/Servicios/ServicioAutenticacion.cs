namespace CentroEventos.Aplicacion.Servicios;

public class ServicioAutenticacion
{
    public bool VerificarContraseña(string contraseña, string hashAComparar)
    {
        string hash = Hasher.Hashear(contraseña);
        if (hash == hashAComparar)
        {
            return true;
        }
        return false;
    }

}