using System;
using CentroEventos.Aplicacion.Repositorios;
using CentroEventos.Aplicacion.Servicios;

namespace CentroEventos.Aplicacion.CasosDeUso.UsuarioU;

public class CerrarSesionUseCase
{
    private readonly UsuarioSesion _sesion;

    public CerrarSesionUseCase(
        UsuarioSesion sesion)
    {
        _sesion = sesion;
    }

    public void Ejecutar()
    {
        _sesion.CerrarSesion();
    }
}

