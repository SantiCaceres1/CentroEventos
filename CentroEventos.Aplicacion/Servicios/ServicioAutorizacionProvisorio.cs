using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Servicios;

namespace CentroEventos.Aplicacion;

public class ServicioAutorizacionProvisorio : IServicioAutorizacion
{
    public bool PoseeElPermiso(int idUsuario, Permiso permiso)
    {
        // return TienePermiso(idUsuario, permiso);
        return true; // Provisionalmente, todos los usuarios tienen todos los permisos
    }
}
