
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Repositorios;

namespace CentroEventos.Aplicacion.Validadores;

public class ValidadorEventoDeportivo
{
    private readonly IRepositorioEventoDeportivo _repoEvento;
    private readonly IRepositorioPersona _repoPersona;

    public ValidadorEventoDeportivo(IRepositorioEventoDeportivo repoEvento, IRepositorioPersona repoPersona)
    {
        _repoEvento = repoEvento;
        _repoPersona = repoPersona;
    }

    public async Task<List<string>> Validar(EventoDeportivo evento)
    {
        var errores = new List<string>();

        if(string.IsNullOrWhiteSpace(evento.Nombre))
            errores.Add("El nombre del evento no puede estar vacio.");

        if(string.IsNullOrWhiteSpace(evento.Descripcion))
            errores.Add("La descripcion del evento no puede estar vacia.");

        if(evento.CupoMaximo <=0)
            errores.Add("El cupo maximo debe ser mayor que 0.");

        if(evento.DuracionHoras<=0)
            errores.Add("La duracion debe ser mayor que cero.");

        if(evento.FechaInicio < DateTime.Now)
            errores.Add("La fecha del evento debe ser futura o actual.");

        if (_repoPersona.ObtenerPorId(evento.IdResponsable)== null)
            errores.Add("No existe la persona responsable indicada.");

        return errores;
    }
}
