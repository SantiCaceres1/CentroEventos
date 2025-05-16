using System;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Repositorios;
using CentroEventos.Aplicacion.Servicios;
using CentroEventos.Aplicacion.Validadores;

namespace CentroEventos.Aplicacion.CasosDeUso.PersonaU;

public class ModificarPersonaUseCase
{
    private readonly IRepositorioPersona _repositorio;
    private readonly IServicioAutorizacion _autorizacion;
    private readonly ValidadorPersona _validador;

    public ModificarPersonaUseCase(IRepositorioPersona repositorio, IServicioAutorizacion autorizacion)
    {
        _repositorio = repositorio;
        _autorizacion = autorizacion;
        _validador = new ValidadorPersona(repositorio);
    }

    public void Ejecutar(Persona persona, int idUsuario)
    {
        if (!_autorizacion.PoseeElPermiso(idUsuario, Permiso.UsuarioModificacion))
            throw new FalloAutorizacionException("El usuario no tiene permiso para modificar personas.");

        if (!_repositorio.ExisteId(persona.Id))
            throw new EntidadNotFoundException("No existe la persona que se quiere modificar.");

        _validador.Validar(persona);

        _repositorio.Modificar(persona);

    }
}
