using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Repositorios;

namespace CentroEventos.Aplicacion.CasosDeUso.UsuarioU;

public class ListarUsuarioUseCase(IRepositorioUsuario repositorio)
{
    private IRepositorioUsuario _repositorio = repositorio;

    public List<Usuario> Ejecutar()
    {
        return _repositorio.ListarTodas();
    }
}
