using System;
using CentroEventos.Aplicacion.Entidades;

namespace CentroEventos.Aplicacion.Repositorios;

public interface IRepositorioEventoDeportivo
{
    void Agregar(EventoDeportivo eventoDeportivo);
    void Modificar(EventoDeportivo eventoDeportivo);
    void Eliminar(int  idEvento);

    EventoDeportivo? ObtenerPorId(int id);
    
    List<EventoDeportivo> ListarTodos();
    bool ExisteId(int id);
    bool HayCupoDisponible(int id);

}
