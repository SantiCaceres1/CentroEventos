using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Repositorios;

public class ValidadorReserva
{
    private IRepositorioReserva _repoReserva;
    private IRepositorioPersona _repoPersona;
    private IRepositorioEventoDeportivo _repoEvento;

    public ValidadorReserva(
        IRepositorioReserva repoReserva,
        IRepositorioPersona repoPersona,
        IRepositorioEventoDeportivo repoEvento)
    {
        _repoReserva = repoReserva;
        _repoPersona = repoPersona;
        _repoEvento = repoEvento;
    }

    public List<string> Validar(Reserva reserva)
    {
        var errores = new List<string>();
        var persona = _repoPersona.ObtenerPorId(reserva.IdPersona);
        if (persona is null)
            errores.Add("No existe la persona indicada.");
        var evento = _repoEvento.ObtenerPorId(reserva.IdEventoDeportivo);
        if (evento is null)
            errores.Add("No existe el evento indicado.");
        if (persona is not null && evento is not null)
        {
            var duplicado = _repoReserva.ExisteReservaDuplicada(reserva.IdPersona, reserva.IdEventoDeportivo);
            if (duplicado)
                errores.Add("La persona ya tiene una reserva para ese evento.");
            var cupoDisponible = _repoEvento.HayCupoDisponible(reserva.IdEventoDeportivo);
            if (!cupoDisponible)
                errores.Add("No hay cupo disponible para ese evento.");
        }
        return errores;
    }
}