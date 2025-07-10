using System;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Repositorios;
using CentroEventos.Aplicacion.Servicios;

namespace CentroEventos.Aplicacion.CasosDeUso.UsuarioU;

public class UsuarioEsAdminUseCase
{
    private readonly IRepositorioUsuario _repositorio;
    private readonly UsuarioSesion _sesion;

    public UsuarioEsAdminUseCase(IRepositorioUsuario repositorio, UsuarioSesion sesion)
    {
        _repositorio = repositorio;
        _sesion = sesion;
    }

    public async Task<bool> Ejecutar()
    {
        var id = _sesion.UsuarioActual?.Id;
        if (id == null) { Console.WriteLine("Id es nulo"); return false; }

        return await _repositorio.PoseeElPermiso(id.Value, Permiso.UsuarioAlta)
            && await _repositorio.PoseeElPermiso(id.Value, Permiso.UsuarioModificacion)
            && await _repositorio.PoseeElPermiso(id.Value, Permiso.UsuarioBaja);
    }
}
