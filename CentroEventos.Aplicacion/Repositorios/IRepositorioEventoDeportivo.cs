using System;
using CentroEventos.Aplicacion.Entidades;

namespace CentroEventos.Aplicacion.Repositorios;

public interface IRepositorioEventoDeportivo
{
    void Agregar(EventoDeportivo evento);
    void Modificar(EventoDeportivo evento);
    void Eliminar(EventoDeportivo evento);

    EventoDeportivo? ObtenerPorId(int id);
    
    List<EventoDeportivo> ListarTodos();
    bool ExisteId(int id);
    bool HayCupoDisponible(int id);

}
