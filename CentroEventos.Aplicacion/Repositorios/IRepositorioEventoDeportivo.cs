using CentroEventos.Aplicacion.Entidades;

namespace CentroEventos.Aplicacion.Repositorios
{
    public interface IRepositorioEventoDeportivo
    {
        Task Agregar(EventoDeportivo evento);
        Task Modificar(EventoDeportivo evento);
        Task Eliminar(int id);
        Task<EventoDeportivo?> ObtenerPorId(int id);
        Task<List<EventoDeportivo>> ListarTodos();
        Task<bool> ExisteId(int id);
        Task<bool> HayCupoDisponible(int id);
    }
}