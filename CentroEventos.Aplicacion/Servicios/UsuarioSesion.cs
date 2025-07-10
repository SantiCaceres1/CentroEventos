using CentroEventos.Aplicacion.Entidades;

namespace CentroEventos.Aplicacion.Servicios
{
    public class UsuarioSesion
    {
        public Usuario? UsuarioActual { get; set; }

        public void IniciarSesion(Usuario usuario)
        {
            UsuarioActual = usuario;
        }

        public bool EstaLogueado()
        {
            if (UsuarioActual != null)
            {
                return true;
            }
            return false;
        }

        public Usuario UsuarioLogueado => UsuarioActual
            ?? throw new InvalidOperationException("No hay usuario logueado.");

        public void CerrarSesion()
        {
            UsuarioActual = null;
        }

        public int? Id => UsuarioActual?.Id;
    }
}

