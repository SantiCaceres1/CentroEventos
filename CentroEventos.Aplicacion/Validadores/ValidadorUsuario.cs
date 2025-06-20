using System.Text.RegularExpressions;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Repositorios;

namespace CentroEventos.Aplicacion.Validadores;

public class ValidadorUsuario
{  
    private readonly IRepositorioUsuario _repositorio;

    public ValidadorUsuario(IRepositorioUsuario repositorio)
    {
        _repositorio = repositorio;
    }

    private bool CorreoElectronicoValido(string CorreoElectronico)
    {
        return Regex.IsMatch(CorreoElectronico, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }

    public void Validar(Usuario usuario)
    {
        if (string.IsNullOrWhiteSpace(usuario.Nombre))
            throw new  ValidacionException("El nombre es obligatorio.");
        if (string.IsNullOrWhiteSpace(usuario.Apellido))
            throw new  ValidacionException("El apellido es obligatorio.");
        if (string.IsNullOrWhiteSpace(usuario.CorreoElectronico))
            throw new  ValidacionException("El correo electrónico es obligatorio.");
        if (string.IsNullOrWhiteSpace(usuario.HashContraseña))
            throw new  ValidacionException("La contraseña es obligatoria.");
        if (!CorreoElectronicoValido(usuario.CorreoElectronico))
            throw new  ValidacionException("El correo electrónico no tiene un formato válido");
        if (_repositorio.ExisteCorreoElectronico(usuario.CorreoElectronico) != null)
            throw new DuplicadoException("Ya existe un usuario con el mismo email.");
    }
}