
using System.IO;
using System.Collections.Generic;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Repositorios;

namespace CentroEventos.Repositorios.Repositorios
{
    public class RepositorioPersona : IRepositorioPersona
    {
        private const string ArchivoPersonas = "personas.csv";
        private const string ArchivoId = "personas_ID.csv";

        public RepositorioPersona()
        {
            if (!File.Exists(ArchivoPersonas))
                File.WriteAllText(ArchivoPersonas, "id;dni;nombre;apellido;email;telefono\n");

            if (!File.Exists(ArchivoId))
                File.WriteAllText(ArchivoId, "1");
        }

        public void Agregar(Persona persona)
        {
            if (ExisteDni(persona.Dni) || ExisteEmail(persona.Email))
                return;

            int id = int.Parse(File.ReadAllText(ArchivoId));
            File.WriteAllText(ArchivoId, (id + 1).ToString());

            string linea = $"{id};{persona.Dni};{persona.Nombre};{persona.Apellido};{persona.Email};{persona.Telefono}";
            File.AppendAllText(ArchivoPersonas, linea + "\n");
        }

        public void Modificar(Persona persona)
        {
            var nuevasLineas = new List<string>();
            nuevasLineas.Add("id;dni;nombre;apellido;email;telefono");

            foreach (var linea in LeerLineasDePersonas())
            {
                string[] campos = linea.Split(";");
                if (int.Parse(campos[0]) == persona.Id)
                {
                    string modificado = $"{persona.Id};{persona.Dni};{persona.Nombre};{persona.Apellido};{persona.Email};{persona.Telefono}";
                    nuevasLineas.Add(modificado);
                }
                else
                {
                    nuevasLineas.Add(linea);
                }
            }

            File.WriteAllLines(ArchivoPersonas, nuevasLineas);
        }

        public void Eliminar(int idPersona)
        {
            var nuevasLineas = new List<string> { "id;dni;nombre;apellido;email;telefono" };

            foreach (var linea in LeerLineasDePersonas())
            {
                string[] campos = linea.Split(";");
                if (int.Parse(campos[0]) != idPersona)
                    nuevasLineas.Add(linea);
            }

            File.WriteAllLines(ArchivoPersonas, nuevasLineas);
        }

        public Persona? ObtenerPorId(int id)
        {
            foreach (var linea in LeerLineasDePersonas())
            {
                var campos = linea.Split(";");
                if (int.Parse(campos[0]) == id)
                    return new Persona(id, campos[1], campos[2], campos[3], campos[4], campos[5]);
            }
            return null;
        }

        public Persona? ObtenerPorDni(string dni)
        {
            foreach (var linea in LeerLineasDePersonas())
            {
                var campos = linea.Split(";");
                if (campos[1] == dni)
                    return new Persona(int.Parse(campos[0]), campos[1], campos[2], campos[3], campos[4], campos[5]);
            }
            return null;
        }

        public Persona? ObtenerPorEmail(string email)
        {
            foreach (var linea in LeerLineasDePersonas())
            {
                var campos = linea.Split(";");
                if (campos[4] == email)
                    return new Persona(int.Parse(campos[0]), campos[1], campos[2], campos[3], campos[4], campos[5]);
            }
            return null;
        }

        public List<Persona> ListarTodas()
        {
            var lista = new List<Persona>();
            foreach (var linea in LeerLineasDePersonas())
            {
                var campos = linea.Split(";");
                lista.Add(new Persona(int.Parse(campos[0]), campos[1], campos[2], campos[3], campos[4], campos[5]));
            }
            return lista;
        }

        public bool ExisteDni(string dni)
        {
            foreach (var linea in LeerLineasDePersonas())
                if (linea.Split(";")[1] == dni)
                    return true;
            return false;
        }

        public bool ExisteEmail(string email)
        {
            foreach (var linea in LeerLineasDePersonas())
                if (linea.Split(";")[4] == email)
                    return true;
            return false;
        }

        public bool ExisteId(int id)
        {
            foreach (var linea in LeerLineasDePersonas())
                if (int.Parse(linea.Split(";")[0]) == id)
                    return true;
            return false;
        }

        private List<string> LeerLineasDePersonas()
        {
            var lineas = new List<string>();
            if (!File.Exists(ArchivoPersonas)) return lineas;

            using var sr = new StreamReader(ArchivoPersonas);
            sr.ReadLine(); // skip header
            while (!sr.EndOfStream)
            {
                var linea = sr.ReadLine();
                if (!string.IsNullOrWhiteSpace(linea))
                    lineas.Add(linea);
            }
            return lineas;
        }
    }
}
