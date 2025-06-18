using CentroEventos.Aplicacion.Entidades;

namespace CentroEventos.Aplicacion.Repositorios
{
    public interface IRepositorioReserva
    {
        Task Agregar(Reserva reserva);
        Task Modificar(Reserva reserva);
        Task Eliminar(int id);
        Task<Reserva?> ObtenerPorId(int id);
        Task<List<Reserva>> ListarTodas();
        Task<bool> ExisteReserva(int id);
        Task<bool> ExisteReservaDuplicada(int personaId, int eventoId);
        Task<int> ContarReservaParaEvento(int eventoId);
    }
}