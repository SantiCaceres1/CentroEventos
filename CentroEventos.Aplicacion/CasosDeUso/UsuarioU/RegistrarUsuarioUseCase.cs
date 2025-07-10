using System;
using CentroEventos.Aplicacion.Repositorios;
using CentroEventos.Aplicacion.Entidades;


namespace CentroEventos.Aplicacion.CasosDeUso.UsuarioU;

public class RegistrarUsuarioUseCase
 {
        private readonly IRepositorioUsuario _repositorio;

        public RegistrarUsuarioUseCase(IRepositorioUsuario repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<bool> Ejecutar(string nombre, string correo, string contrasenia)
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
