using System;
using CentroEventos.Aplicacion.Entidades;

namespace CentroEventos.Aplicacion.Repositorios;

public interface IRepositorioReserva
{
    void Agregar (Reserva reserva); 
    void Modificar (Reserva reserva);
    void Eliminar( int id);

    Reserva? ObtenerPorId(int id);
    List<Reserva> ListarTodas();
    bool ExisteResercaDuplicada(int IdPersona,int IdEventoDeportivo);
    int ContarReservaParaEvento(int IdEventoDeportivo);
}
