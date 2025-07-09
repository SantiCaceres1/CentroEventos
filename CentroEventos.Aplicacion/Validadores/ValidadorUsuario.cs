using System.Text.RegularExpressions;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Repositorios;

public class ValidadorUsuario
{
    private readonly IRepositorioUsuario _repositorio;

    public ValidadorUsuario(IRepositorioUsuario repositorio)
    {
        _repositorio = repositorio;
    }

    private bool CorreoElectronicoValido(string correo)
    {
        return Regex.IsMatch(correo, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }

    public async Task<List<string>> Validar(Usuario usuario)
    {
        var errores = new List<string>();

        if (string.IsNullOrWhiteSpace(usuario.Nombre))
            errores.Add("El nombre es obligatorio.");

        if (string.IsNullOrWhiteSpace(usuario.Apellido))
            errores.Add("El apellido es obligatorio.");

        if (string.IsNullOrWhiteSpace(usuario.CorreoElectronico))
            errores.Add("El correo electrónico es obligatorio.");

        if (string.IsNullOrWhiteSpace(usuario.HashContraseña))
            errores.Add("La contraseña es obligatoria.");

        if (!CorreoElectronicoValido(usuario.CorreoElectronico))
            errores.Add("El correo electrónico no tiene un formato válido.");

        if (await _repositorio.ExisteCorreoElectronico(usuario.CorreoElectronico))
            errores.Add("Ya existe un usuario con el mismo email.");

        return errores;
    }
}