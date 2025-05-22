using System.IO;
using System.Collections.Generic;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Repositorios;


namespace CentroEventos.Repositorios.Repositorios
{
    public class RepositorioPersona : IRepositorioPersona
    {
        public RepositorioPersona(string str)
        {
            File.WriteAllText("personas.csv",str);
        }
        public void Agregar(Persona? persona)
        {
            if (!this.ExisteDni(persona.Dni) && !this.ExisteEmail(persona.Email))
            {
                int id;
                if (!File.Exists("personas_ID.csv"))
                {
                    id = 1;
                    File.WriteAllText("personas_ID.csv", id.ToString());
                }
                else
                {
                    id = int.Parse(File.ReadAllText("personas_ID.csv"));
                    File.WriteAllText("personas_ID.csv", (id + 1).ToString());
                }
                if (!File.Exists("personas.csv") || new FileInfo("personas.csv").Length == 0)
                {
                    File.AppendAllText("personas.csv", "id;dni;nombre;apellido;email;telefono\n");
                }
                string linea = $"{id};{persona.Dni};{persona.Nombre};{persona.Apellido};{persona.Email};{persona.Telefono}";
                File.AppendAllText("personas.csv", linea + "\n");
            }
        }

        public void Modificar(Persona? persona)
        {
            var nuevasLineas = new List<string>();
            foreach (var linea in LeerLineasDePersonas())
            {
                string[] campos = linea.Split(";");
                if (campos.Length != 6) continue;
                int id_persona = int.Parse(campos[0]);
                if (id_persona == persona.Id)
                {
                    string? dni =  persona.Dni;
                    string? nombre = persona.Nombre;
                    string?  apellido = persona.Apellido;
                    string? email = persona.Email;
                    string telefono =  persona.Telefono;
                    var lineaModificada = $"{id_persona};{persona.Dni};{persona.Nombre};{persona.Apellido};{persona.Email};{persona.Telefono}";
                    nuevasLineas.Add(lineaModificada);
                }
                else
                {
                    nuevasLineas.Add(linea);
                }
            }
            nuevasLineas.Insert(0, "id;dni;nombre;apellido;email;telefono");
            File.WriteAllLines("personas.csv", nuevasLineas);
        }

        public void Eliminar(int idPersona)
        {
            var nuevasLineas = new List<string>();
            foreach (var linea in LeerLineasDePersonas())
            {
                string[] campos = linea.Split(";");
                if (campos.Length != 6) continue;
                int id_persona = int.Parse(campos[0]);
                if (id_persona != idPersona)
                {
                    nuevasLineas.Add(linea);
                }
            }
            nuevasLineas.Insert(0, "id;dni;nombre;apellido;email;telefono");
            File.WriteAllLines("personas.csv", nuevasLineas);
        }

        public Persona? ObtenerPorId(int id)
        {
            foreach (var linea in LeerLineasDePersonas())
            {
                string[] campos = linea.Split(";");
                if (campos.Length != 6) continue;
                int id_persona = int.Parse(campos[0]);
                if (id_persona == id)
                {
                    string dni = campos[1];
                    string nombre = campos[2];
                    string apellido = campos[3];
                    string email = campos[4];
                    string telefono = campos[5];
                    Persona persona = new Persona(id_persona, nombre, apellido, dni, email, telefono);
                    return persona;
                }
            }
            return null;
        }

        public Persona? ObtenerPorDni(string dni)
        {
            foreach (var linea in LeerLineasDePersonas())
            {
                string[] campos = linea.Split(";");
                if (campos.Length != 6) continue;
                int id = int.Parse(campos[0]);
                string dni_persona = campos[1];
                if (dni_persona == dni)
                {
                    string nombre = campos[2];
                    string apellido = campos[3];
                    string email = campos[4];
                    string telefono = campos[5];
                    Persona persona = new Persona(id, nombre, apellido, dni_persona, email, telefono);
                    return persona;
                }
            }
            return null;
        }

        public Persona? ObtenerPorEmail(string email)
        {
            foreach (var linea in LeerLineasDePersonas())
            {
                string[] campos = linea.Split(";");
                if (campos.Length != 6) continue;
                int id = int.Parse(campos[0]);
                string dni = campos[1];
                string nombre = campos[2];
                string apellido = campos[3];
                string email_persona = campos[4];
                if (email_persona == email)
                {
                    string telefono = campos[5];
                    Persona persona = new Persona(id, nombre, apellido, dni, email_persona, telefono);
                    return persona;
                }
            }
            return null;
        }

        public List<Persona> ListarTodas()
        {
            List<Persona> lista = new List<Persona>();
            foreach (var linea in LeerLineasDePersonas())
            {
                string[] campos = linea.Split(';');
                if (campos.Length != 6) continue;
                int id = int.Parse(campos[0]);
                string dni = campos[1];
                string nombre = campos[2];
                string apellido = campos[3];
                string email = campos[4];
                string telefono = campos[5];
                Persona persona = new Persona(id, nombre, apellido, dni, email, telefono);
                lista.Add(persona);
            }
            return lista;
        }

        public bool ExisteDni(string dni)
        {
            foreach (var linea in LeerLineasDePersonas())
            {
                string[] campos = linea.Split(';');
                if (campos.Length != 6) continue;
                if (campos[1] == dni)
                    return true;
            }
            return false;
        }

        public bool ExisteEmail(string email)
        {
            foreach (var linea in LeerLineasDePersonas())
            {
                string[] campos = linea.Split(';');
                if (campos.Length != 6) continue;
                if (campos[4] == email)
                    return true;
            }
            return false;
        }

        public bool ExisteId(int id)
        {
            foreach (var linea in LeerLineasDePersonas())
            {
                string[] campos = linea.Split(';');
                if (campos.Length != 6) continue;
                if (int.TryParse(campos[0], out int idArchivo) && idArchivo == id)
                    return true;
            }
            return false;
        }

        private List<string> LeerLineasDePersonas()
        {
            List<string> lineas = new List<string>();
            if (!System.IO.File.Exists("personas.csv"))
                return lineas;
            using var sr = new System.IO.StreamReader("personas.csv");
            sr.ReadLine();
            while (!sr.EndOfStream)
            {
                string? linea = sr.ReadLine();
                if (!string.IsNullOrWhiteSpace(linea))
                {
                    lineas.Add(linea);
                }
            }
            return lineas;
        }
    }
}