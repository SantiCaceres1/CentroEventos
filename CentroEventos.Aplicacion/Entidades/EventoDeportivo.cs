using System;

namespace CentroEventos.Aplicacion.Entidades;

public class EventoDeportivo
{
    private int _id;

    private string? _nombre;

    private string? _descripcion;

    private DateTime _fechaInicio ;

    private double _duracionHoras ;

    private int _cupoMaximo ;

    private int _idResponsable ;

    public int Id => _id;

    public string? Nombre => _nombre;

    public string? Descripcion => _descripcion;

    public DateTime  FechaInicio => _fechaInicio;

    public double DuracionHoras => _duracionHoras;

    public int CupoMaximo => _cupoMaximo;

    public int IdResponsable => _idResponsable;

    public override string ToString()
    {
        return $"[{_id}] {_nombre} {_fechaInicio} - {_duracionHoras}HS - Cupo: {_cupoMaximo}";
    }

}
