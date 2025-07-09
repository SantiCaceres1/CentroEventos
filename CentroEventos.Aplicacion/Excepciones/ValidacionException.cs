namespace CentroEventos.Aplicacion.Excepciones;

public class ValidacionException : Exception
{
    public ValidacionException(List<string> mensajes) : base(string.Join(", ", mensajes)){}
}