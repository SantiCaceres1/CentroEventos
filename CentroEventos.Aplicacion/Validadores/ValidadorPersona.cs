using System;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Repositorios;

namespace CentroEventos.Aplicacion.Validadores;

public class ValidadorPersona
{
     private readonly IRepositorioPersona _reporsitorio;

    public ValidadorPersona(IRepositorioPersona repositorio)
    {
        _reporsitorio = repositorio;
    }

    public void Validar(Persona persona)
    {
        if(string.IsNullOrWhiteSpace(persona.Dni))
            throw new ValidacionException("El DNI no puede estar vacio.");

        if(string.IsNullOrWhiteSpace(persona.Nombre))
            throw new ValidacionException("El nombre no puede estar vacio.");

        if(string.IsNullOrWhiteSpace(persona.Apellido))
            throw new ValidacionException("El apellido no puede estar vacio.");

        if(string.IsNullOrWhiteSpace(persona.Email))
            throw new ValidacionException("El email no puede estar vacio.");

        if(_reporsitorio.ExisteDni(persona.Dni))
            throw new DuplicadoException("Ya existe una persona con el mismo DNI.");

        if(_reporsitorio.ExisteEmail(persona.Email))
            throw new DuplicadoException("Ya existe unapersona con el mismo email.");
        
    }
}
