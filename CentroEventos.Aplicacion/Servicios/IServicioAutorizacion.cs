
using CentroEventos.Aplicacion.Entidades;

namespace CentroEventos.Aplicacion.Servicios;

public interface IServicioAutorizacion
{
    bool PoseeElPermiso(int idUsuario, Permiso permiso);
}
