
using CentroEventos.Aplicacion.Entidades;

namespace CentroEventos.Aplicacion.Repositorios;

public interface IRepositorioReserva
{
    void Agregar (Reserva reserva); 
    void Modificar (Reserva reserva);
    void Eliminar( int idReserva);

    Reserva? ObtenerPorId(int id);
    List<Reserva> ListarTodas();
    bool ExisteReserva(int idReserva);
    bool ExisteReservaDuplicada(int IdPersona,int IdEventoDeportivo);
    int ContarReservaParaEvento(int IdEventoDeportivo);
}
