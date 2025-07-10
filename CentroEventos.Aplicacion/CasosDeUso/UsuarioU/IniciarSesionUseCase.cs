using System;
using CentroEventos.Aplicacion.Repositorios;
using CentroEventos.Aplicacion.Servicios;

namespace CentroEventos.Aplicacion.CasosDeUso.UsuarioU;

public class IniciarSesionUseCase
{
    private readonly IRepositorioUsuario _repositorio;
    private readonly ServicioAutenticacion _autenticacion;
    private readonly UsuarioSesion _sesion;

    public IniciarSesionUseCase(
        IRepositorioUsuario repositorio,
        ServicioAutenticacion autenticacion,
        UsuarioSesion sesion)
    {
        _repositorio = repositorio;
        _autenticacion = autenticacion;
        _sesion = sesion;
    }

    public async Task<bool> Ejecutar(string correo, string contraseña)
    {
        if (string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(contraseña))
            return false;

        var usuario = await _repositorio.ObtenerPorCorreoElectronico(correo);
        if (usuario != null && _autenticacion.VerificarContraseña(contraseña, usuario.HashContraseña!))
        {
            _sesion.UsuarioActual = usuario;
            return true;
        }

        return false;
    }
}
