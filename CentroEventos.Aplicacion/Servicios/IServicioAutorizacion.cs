
using CentroEventos.Aplicacion.Entidades;

namespace CentroEventos.Aplicacion.Servicios;

public interface IServicioAutorizacion
{
    Task<bool> PoseeElPermiso(int idUsuario, Permiso permiso);
}