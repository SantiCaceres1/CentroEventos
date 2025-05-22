
using System.Collections.Generic;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Repositorios;

namespace CentroEventos.Repositorios.Repositorios
{
    public class RepositorioEventoDeportivo : IRepositorioEventoDeportivo
    {
        public RepositorioEventoDeportivo(string str)
        {
            File.WriteAllText("eventos.csv", str);
        }
        public void Agregar(EventoDeportivo? evento)
        {
            int id;
            if (!File.Exists("eventos_ID.csv"))
            {
                id = 1;
                File.WriteAllText("eventos_ID.csv", id.ToString());
            }
            else
            {
                id = int.Parse(File.ReadAllText("eventos_ID.csv"));
                File.WriteAllText("eventos_ID.csv", (id + 1).ToString());
            }
            if (!File.Exists("eventos.csv") || new FileInfo("eventos.csv").Length == 0)
            {
                File.AppendAllText("eventos.csv", "id;nombre;descripcion;fechaInicio;duracionHoras;cupoMaximo;idResponsable\n");
            }
            string linea = $"{id};{evento.Nombre};{evento.Descripcion};{evento.FechaInicio};{evento.DuracionHoras};{evento.CupoMaximo};{evento.IdResponsable}";
            File.AppendAllText("eventos.csv", linea + "\n");
        }

        public void Modificar(EventoDeportivo? evento)
        {
            List<string> lineas = new List<string>();
            if (File.Exists("eventos.csv"))
            {
                foreach (var linea in this.LeerLineasDeEventos())
                {
                    string[] campos = linea.Split(";");
                    if (campos.Length != 7)
                    {
                        lineas.Add(linea);
                        continue;
                    }
                    int id_evento = int.Parse(campos[0]);
                    if (id_evento == evento.Id)
                    {
                        string nuevaLinea = $"{id_evento};{evento.Nombre};{evento.Descripcion};{evento.FechaInicio};{evento.DuracionHoras};{evento.CupoMaximo};{evento.IdResponsable}";
                        lineas.Add(nuevaLinea);
                    }
                    else
                    {
                        lineas.Add(linea);
                    }
                }
                File.WriteAllText("eventos.csv", "id;nombre;descripcion;fechaInicio;duracionHoras;cupoMaximo;idResponsable\n");
                File.AppendAllLines("eventos.csv", lineas);
            }
        }

        public void Eliminar(int id)
        {
            List<string> lineas = new List<string>();
            if (File.Exists("eventos.csv"))
            {
                foreach (var linea in this.LeerLineasDeEventos())
                {
                    string[] campos = linea.Split(";");
                    if (campos.Length != 7)
                    {
                        lineas.Add(linea);
                        continue;
                    }
                    int id_evento = int.Parse(campos[0]);
                    if (id_evento != id)
                    {
                        lineas.Add(linea);
                    }
                }
            }
            File.WriteAllText("eventos.csv", "id;nombre;descripcion;fechaInicio;duracionHoras;cupoMaximo;idResponsable\n");
            File.AppendAllLines("eventos.csv", lineas);
        }

        public EventoDeportivo? ObtenerPorId(int id)
        {
            foreach (var linea in this.LeerLineasDeEventos())
            {
                string[] campos = linea.Split(";");
                if (campos.Length != 7) continue;
                int id_evento = int.Parse(campos[0]);
                if (id_evento == id)
                {
                    string nombre = campos[1];
                    string descripcion = campos[2];
                    DateTime fechaInicio = DateTime.Parse(campos[3]);
                    double horas = double.Parse(campos[4]);
                    int cupo = int.Parse(campos[5]);
                    int id_Responsable = int.Parse(campos[6]);
                    EventoDeportivo evento = new EventoDeportivo(id_evento, nombre, descripcion, fechaInicio, horas, cupo, id_Responsable);
                    return evento;
                }
            }
            return null;
        }

        public List<EventoDeportivo> ListarTodos()
        {
            List<EventoDeportivo> lista = new List<EventoDeportivo>();
            foreach (var linea in this.LeerLineasDeEventos())
            {
                string[] campos = linea.Split(";");
                if (campos.Length != 7) continue;
                int id = int.Parse(campos[0]);
                string nombre = campos[1];
                string descripcion = campos[2];
                DateTime fechaInicio = DateTime.Parse(campos[3]);
                double horas = double.Parse(campos[4]);
                int cupo = int.Parse(campos[5]);
                int id_Responsable = int.Parse(campos[6]);
                EventoDeportivo evento = new EventoDeportivo(id, nombre, descripcion, fechaInicio, horas, cupo, id_Responsable);
                lista.Add(evento);
            }
            return lista;
        }
        public bool ExisteId(int id)
        {
            if (this.ObtenerPorId(id) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool HayCupoDisponible(int id)
        {
            EventoDeportivo? evento = this.ObtenerPorId(id);
            if (evento == null)
                return false;
            int contador = 0;
            if (File.Exists("reservas.csv"))
            {
                using var sr = new StreamReader("reservas.csv");
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    var linea = sr.ReadLine();
                    if (!string.IsNullOrWhiteSpace(linea))
                    {
                        var campos = linea.Split(";");
                        if (campos.Length >= 3)
                        {
                            int idEventoReserva = int.Parse(campos[2]);
                            if (idEventoReserva == id)
                            {
                                contador++;
                            }
                        }
                    }
                }
            }
            return contador < evento.CupoMaximo;
        }

        private List<string> LeerLineasDeEventos()
        {
            List<string> lineas = new List<string>();
            if (!System.IO.File.Exists("eventos.csv"))
            {
                return lineas;
            }
            else
            {
                var sr = new StreamReader("eventos.csv");
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    var linea = sr.ReadLine();
                    if (!string.IsNullOrWhiteSpace(linea))
                    {
                        lineas.Add(linea);
                    }
                }
                sr.Close();
                return lineas;
            }
        }
    }
}