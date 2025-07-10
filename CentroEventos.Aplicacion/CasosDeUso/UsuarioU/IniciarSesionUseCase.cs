using System;
using CentroEventos.Aplicacion.Repositorios;
using CentroEventos.Aplicacion.Servicios;

namespace CentroEventos.Aplicacion.CasosDeUso.UsuarioU;

public class IniciarSesionUseCase
{
    
    private readonly IRepositorioUsuario _repo;
    private readonly ServicioAutenticacion _autenticacion;
    private readonly UsuarioSesion _sesion;

    public IniciarSesionUseCase(
        IRepositorioUsuario repo,
        ServicioAutenticacion autenticacion,
        UsuarioSesion sesion)
    {
        _repo = repo;
        _autenticacion = autenticacion;
        _sesion = sesion;
    }

    public async Task<bool> Ejecutar(string correo, string contrasenia)
    {
        var usuario = await _repo.ObtenerPorCorreoElectronico(correo);
        if (usuario is null) return false;

        if (!_autenticacion.VerificarContraseña(contrasenia, usuario.HashContraseña!))
            return false;

        _sesion.IniciarSesion(usuario); // 👈 ESTA LÍNEA ES CLAVE
        return true;
    }
}

