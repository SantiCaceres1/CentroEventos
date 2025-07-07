using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Repositorios;

namespace CentroEventos.Aplicacion.Servicios
{
    public class UsuarioSesion
    {
        private readonly IRepositorioUsuario _repositorio;

        public Usuario? UsuarioActual { get; private set; }

        public UsuarioSesion(IRepositorioUsuario repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<bool> IniciarSesion(string correo, string contraseña)
        {
            if (string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(contraseña))
                return false;

            var usuario = await _repositorio.ObtenerPorCorreoElectronico(correo);

            if (usuario != null && _servicio.VerificarContraseña(contraseña, usuario.HashContraseña))
            {
                UsuarioActual = usuario;
                return true;
            }

            return false;
        }

        public void CerrarSesion()
        {
            UsuarioActual = null;
        }

        public bool EstaLogueado() => UsuarioActual != null;

        public async Task<bool> UsuarioEsAdmin()
        {
            if (UsuarioActual == null)
                return false;

            bool tieneAlta = await _repositorio.PoseeElPermiso(UsuarioActual.ID, Permiso.UsuarioAlta);
            bool tieneModificacion = await _repositorio.PoseeElPermiso(UsuarioActual.ID, Permiso.UsuarioModificacion);
            bool tieneBaja = await _repositorio.PoseeElPermiso(UsuarioActual.ID, Permiso.UsuarioBaja);

            return tieneAlta && tieneModificacion && tieneBaja;
        }

        public Usuario Usuario => UsuarioActual ?? throw new InvalidOperationException("No hay usuario logueado.");
        
        public bool RegistrarUsuario(string nombre, string correo, string contrasenia)
        {
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(contrasenia))
                return false;

            // Validar que no exista un usuario con ese correo
            var existente = _repositorio.ObtenerPorCorreoElectronico(correo);
            if (existente != null)
                return false;

            // Crear nuevo usuario con hash
            var nuevoUsuario = new Usuario(nombre, "", correo, contrasenia); // Apellido vacío por ahora
            _repositorio.Agregar(nuevoUsuario);

            return true;
        }
    }
}