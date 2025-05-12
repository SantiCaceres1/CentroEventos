using System;
using System.Reflection;

namespace CentroEventos.Aplicacion.Entidades;

public class Persona
{
    private int _id {get; set;}  
    private string  _nombre {get; set;}=string.Empty;
    private string  _apellido {get; set;}=string.Empty;
    private string  _DNI {get; set;}=string.Empty;

    private string  _email {get; set;}=string.Empty;

    private long _telefono {get; set;}

    public override string ToString()
    {
        return $"[{_id}] {_nombre} {_apellido} - DNI: {_DNI} - Email: {_email} ";
    }
}
