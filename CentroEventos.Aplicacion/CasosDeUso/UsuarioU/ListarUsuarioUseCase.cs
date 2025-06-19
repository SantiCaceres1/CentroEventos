using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Repositorios;

namespace CentroEventos.Aplicacion.CasosDeUso.UsuarioU;

public class ListarUsuarioUseCase(IRepositorioUsuario repositorio)
{
    private readonly IRepositorioUsuario _repositorio = repositorio;

    public async Task<List<Usuario>> Ejecutar()
    {
        return await _repositorio.ListarTodas();
    }
}
