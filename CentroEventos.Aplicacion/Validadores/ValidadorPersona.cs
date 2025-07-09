using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Repositorios;

namespace CentroEventos.Aplicacion.Validadores;

public class ValidadorPersona
{
    private IRepositorioPersona _repositorio;

    public ValidadorPersona(IRepositorioPersona repositorio)
    {
        _repositorio = repositorio;
    }

    public List<string> Validar(Persona persona)
    {
        var errores = new List<string>();
        if (string.IsNullOrWhiteSpace(persona.Dni))
            errores.Add("El DNI no puede estar vacío.");
        if (string.IsNullOrWhiteSpace(persona.Nombre))
            errores.Add("El nombre no puede estar vacío.");
        if (string.IsNullOrWhiteSpace(persona.Apellido))
            errores.Add("El apellido no puede estar vacío.");
        if (string.IsNullOrWhiteSpace(persona.Email))
            errores.Add("El email no puede estar vacío.");
        if (_repositorio.ObtenerPorDni(persona.Dni) is not null)
            errores.Add("Ya existe una persona con el mismo DNI.");
        if (_repositorio.ObtenerPorEmail(persona.Email) is not null)
            errores.Add("Ya existe una persona con el mismo email.");
        return errores;
    }
}