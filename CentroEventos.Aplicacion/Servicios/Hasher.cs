using System.Security.Cryptography;
using System.Text;

namespace CentroEventos.Aplicacion.Servicios;
public static class Hasher
{
    public static string Hashear(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentException("La entrada para hashear no puede estar vacía.", nameof(input));
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytesEntrada = Encoding.UTF8.GetBytes(input);
            byte[] bytesHash = sha256.ComputeHash(bytesEntrada);
            return Convert.ToBase64String(bytesHash);
        }
    }
    public static bool Verificar(string input, string hashEsperado)
    {
        if (string.IsNullOrWhiteSpace(hashEsperado))
            throw new ArgumentException("El hash esperado no puede estar vacío.", nameof(hashEsperado));
        string hashInput = Hashear(input);
        return hashInput == hashEsperado;
    }
}