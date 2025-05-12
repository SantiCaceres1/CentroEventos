using System;

namespace CentroEventos.Aplicacion.Entidades;

public class EventoDeportivo
{
    private int _id {get; set;}

    private string _nombre {get; set;}= string.Empty;

    private string _descripcion {get; set;}= string.Empty;

    private DateTime _fechaInicio {get; set;}

    private double _duracionHoras {get; set;}

    private int _cupoMaximo {get; set;}

    private int _idResponsable {get;set;}

    public override string ToString()
    {
        return $"[{_id}] {_nombre} {_fechaInicio} - {_duracionHoras}HS - Cupo: {_cupoMaximo}";
    }

}
