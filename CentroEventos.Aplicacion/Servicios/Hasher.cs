using System.Security.Cryptography;
using System.Text;

namespace CentroEventos.Aplicacion.Servicios
{
    public static class Hasher
    {
        // <summary>
        // Aplica SHA-256 a un string de entrada y devuelve el hash en Base64.
        // </summary>
        // <param name="input">Texto a hashear (por ejemplo, contraseña)</param>
        // <returns>Hash en Base64</returns>
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

        // <summary>
        // Compara de forma segura un texto con un hash esperado.
        // </summary>
        // <param name="input">Texto plano (ej: contraseña ingresada)</param>
        // <param name="hashEsperado">Hash guardado</param>
        // <returns>true si coinciden, false si no</returns>
        public static bool Verificar(string input, string hashEsperado)
        {
            if (string.IsNullOrWhiteSpace(hashEsperado))
                throw new ArgumentException("El hash esperado no puede estar vacío.", nameof(hashEsperado));

            string hashInput = Hashear(input);
            return hashInput == hashEsperado;
        }
    }
}