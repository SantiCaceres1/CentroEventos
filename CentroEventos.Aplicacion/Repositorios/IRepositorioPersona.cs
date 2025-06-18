using CentroEventos.Aplicacion.Entidades;

namespace CentroEventos.Aplicacion.Repositorios
{
    public interface IRepositorioPersona
    {
        Task Agregar(Persona persona);
        Task Modificar(Persona persona);
        Task Eliminar(int id);
        Task<Persona?> ObtenerPorId(int id);
        Task<Persona?> ObtenerPorDni(string dni);
        Task<Persona?> ObtenerPorEmail(string email);
        Task<List<Persona>> ListarTodas();
        Task<bool> ExisteDni(string dni);
        Task<bool> ExisteEmail(string email);
    }
}