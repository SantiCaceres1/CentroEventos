using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Repositorios;

public class ObtenerUsuarioUseCase
{
    private readonly IRepositorioUsuario _repositorioUsuario;

    public ObtenerUsuarioUseCase(IRepositorioUsuario repositorioUsuario)
    {
        _repositorioUsuario = repositorioUsuario;
    }

    public async Task<Usuario> Ejecutar(int idUsuario)
    {
        return await _repositorioUsuario.ObtenerPorId(idUsuario);
    }
}