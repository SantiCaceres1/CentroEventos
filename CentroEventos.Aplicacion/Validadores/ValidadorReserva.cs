using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Repositorios;

public class ValidadorReserva
{
    private readonly IRepositorioReserva _repoReserva;
    private readonly IRepositorioPersona _repoPersona;
    private readonly IRepositorioEventoDeportivo _repoEvento;

    public ValidadorReserva(
        IRepositorioReserva repoReserva,
        IRepositorioPersona repoPersona,
        IRepositorioEventoDeportivo repoEvento)
    {
        _repoReserva = repoReserva;
        _repoPersona = repoPersona;
        _repoEvento = repoEvento;
    }

    public async Task<List<string>> Validar(Reserva reserva)
    {
        var errores = new List<string>();

        var persona = await _repoPersona.ObtenerPorId(reserva.IdPersona);
        if (persona is null)
            errores.Add("No existe la persona indicada.");

        var evento = await _repoEvento.ObtenerPorId(reserva.IdEventoDeportivo);
        if (evento is null)
            errores.Add("No existe el evento indicado.");

        if (persona is not null && evento is not null)
        {
            var duplicado = await _repoReserva.ExisteReservaDuplicada(reserva.IdPersona, reserva.IdEventoDeportivo);
            if (duplicado)
                errores.Add("La persona ya tiene una reserva para ese evento.");

            var cupoDisponible = await _repoEvento.HayCupoDisponible(reserva.IdEventoDeportivo);
            if (!cupoDisponible)
                errores.Add("No hay cupo disponible para ese evento.");
        }

        return errores;
    }
}