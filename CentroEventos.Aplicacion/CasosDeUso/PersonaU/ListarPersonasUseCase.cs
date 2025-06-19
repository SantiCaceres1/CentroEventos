using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Repositorios;

namespace CentroEventos.Aplicacion.CasosDeUso.PersonaU;

public class ListarPersonasUseCase
{
    private readonly IRepositorioPersona _repositorio;

    public ListarPersonasUseCase(IRepositorioPersona repositorio)
    {
        _repositorio = repositorio;
    }

    public Task<List<Persona>> Ejecutar()
    {
        return _repositorio.ListarTodas();
    }
}
