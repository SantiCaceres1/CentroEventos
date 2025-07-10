using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Repositorios;

namespace CentroEventos.Aplicacion.Servicios
{
    public class UsuarioSesion
    {
    public Usuario? UsuarioActual { get; set; }
    
        public void IniciarSesionManual(Usuario usuario)
        {
            UsuarioActual = usuario;
        }

        public bool EstaLogueado()
        {
            Console.WriteLine("ESTO ES USER PA");

            if (UsuarioActual != null)
            {
                Console.WriteLine("ESTO ES CIERTO");
                return true;
            }
            Console.WriteLine("ME CAI");
            return false;
        }


    
        public Usuario UsuarioLogueado => UsuarioActual
            ?? throw new InvalidOperationException("No hay usuario logueado.");

    public void CerrarSesion() => UsuarioActual = null;

    public int? Id => UsuarioActual?.Id;
    }
}

