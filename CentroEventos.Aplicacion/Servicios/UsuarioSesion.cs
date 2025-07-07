using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Repositorios;

namespace CentroEventos.Aplicacion.Servicios
{
    public class UsuarioSesion
    {
        private readonly IRepositorioUsuario _repositorio;
        private readonly ServicioAutenticacion _autenticacion;

        public Usuario? UsuarioActual { get; private set; }

        public UsuarioSesion(IRepositorioUsuario repositorio, ServicioAutenticacion autenticacion)
        {
            _repositorio = repositorio;
            _autenticacion = autenticacion;
        }

        public async Task<bool> IniciarSesion(string correo, string contraseña)
        {
            if (string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(contraseña))
                return false;

            var usuario = await _repositorio.ObtenerPorCorreoElectronico(correo);

            if (usuario != null && _autenticacion.VerificarContraseña(contraseña, usuario.HashContraseña!))
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

            var id = UsuarioActual.Id;
            bool tieneAlta = await _repositorio.PoseeElPermiso(id, Permiso.UsuarioAlta);
            bool tieneModificacion = await _repositorio.PoseeElPermiso(id, Permiso.UsuarioModificacion);
            bool tieneBaja = await _repositorio.PoseeElPermiso(id, Permiso.UsuarioBaja);

            return tieneAlta && tieneModificacion && tieneBaja;
        }

        public Usuario UsuarioLogueado => UsuarioActual ?? throw new InvalidOperationException("No hay usuario logueado.");

        public async Task<bool> RegistrarUsuario(string nombre, string correo, string contrasenia)
        {
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(contrasenia))
                return false;

            var existente = await _repositorio.ObtenerPorCorreoElectronico(correo);
            if (existente != null)
                return false;

            var nuevoUsuario = new Usuario(nombre, "", correo, contrasenia); // Apellido vacío
            await _repositorio.Agregar(nuevoUsuario);

            return true;
        }
    }
}
