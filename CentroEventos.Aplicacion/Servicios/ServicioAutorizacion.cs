﻿using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Repositorios;

namespace CentroEventos.Aplicacion.Servicios;

public class ServicioAutorizacion : IServicioAutorizacion
{
    private readonly IRepositorioUsuario _repoUsuario;

    public ServicioAutorizacion(IRepositorioUsuario repoUsuario)
    {
        _repoUsuario = repoUsuario;
    }

    public async Task<bool> PoseeElPermiso(int idUsuario, Permiso permiso)
    {
        return await _repoUsuario.PoseeElPermiso(idUsuario, permiso);
    }
}