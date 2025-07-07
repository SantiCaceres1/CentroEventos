
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Repositorios;

namespace CentroEventos.Aplicacion.Validadores;

public class ValidadorPersona
{
    private readonly IRepositorioPersona _repositorio;

    public ValidadorPersona(IRepositorioPersona repositorio)
    {
        _repositorio = repositorio;
    }

    public async Task<List<string>> Validar(Persona persona)
    {
        var errores = new List<string>();

        if(string.IsNullOrWhiteSpace(persona.Dni))
            errores.Add("El DNI no puede estar vacio.");

        if(string.IsNullOrWhiteSpace(persona.Nombre))
            errores.Add("El nombre no puede estar vacio.");

        if(string.IsNullOrWhiteSpace(persona.Apellido))
            errores.Add("El apellido no puede estar vacio.");

        if(string.IsNullOrWhiteSpace(persona.Email))
            errores.Add("El email no puede estar vacio.");

        if(_repositorio.ObtenerPorDni(persona.Dni) != null)
            errores.Add("Ya existe una persona con el mismo DNI.");

        if(_repositorio.ObtenerPorEmail(persona.Email) != null)
            errores.Add("Ya existe una persona con el mismo email.");

        return errores;
    }
}
