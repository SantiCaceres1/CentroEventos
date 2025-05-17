
using CentroEventos.Aplicacion.Entidades;

namespace CentroEventos.Aplicacion.Repositorios;

public interface IRepositorioPersona
{
     void Agregar(Persona persona);
    void Modificar(Persona persona);
    void Eliminar (int  idPersona);
    Persona? ObtenerPorId(int id);
    Persona? ObtenerPorDni(string dni);
    Persona? ObtenerPorEmail(string email);
    List<Persona> ListarTodas();
    bool ExisteId(int id);
    bool ExisteDni(string dni);
    bool ExisteEmail(string email);
}
