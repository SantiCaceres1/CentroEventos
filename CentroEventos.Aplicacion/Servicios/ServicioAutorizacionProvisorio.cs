using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Servicios;
namespace CentroEventos.Aplicacion;

public class ServicioAutorizacionProvisorio : IServicioAutorizacion
{
    public bool PoseeElPermiso(int idUsuario, Permiso permiso)
    {
        return idUsuario == 1;
    }
}
