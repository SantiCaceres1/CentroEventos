using System.IO;
using System.Collections.Generic;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Repositorios;


namespace CentroEventos.Repositorios.Repositorios
{
    public class RepositorioReserva : IRepositorioReserva
    {
        public RepositorioReserva(string str)
        {
            File.WriteAllText("reservas.csv",str);
        }
        public void Agregar(Reserva? reserva)
        {
            int id;
            if (!File.Exists("reservas_ID.csv"))
            {
                id = 1;
                File.WriteAllText("reservas_ID.csv", id.ToString());
            }
            else
            {
                id = int.Parse(File.ReadAllText("reservas_ID.csv"));
                File.WriteAllText("reservas_ID.csv", (id + 1).ToString());
            }
            reserva.AsignarId(id);
            if (!File.Exists("reservas.csv") || new FileInfo("reservas.csv").Length == 0)
            {
                File.AppendAllText("reservas.csv", "id;idPersona;idEvento;fechaReserva\n");
            }
            string linea = $"{reserva.Id};{reserva.IdPersona};{reserva.IdEventoDeportivo};{reserva.FechaAltaReserva}";
            File.AppendAllText("reservas.csv", linea + "\n");
        }

        public void Modificar(Reserva? reserva)
        {
            List<string> lineas = new List<string>();
            lineas.Add("id;idPersona;idEvento;fechaReserva");
            foreach (var linea in this.LeerLineasDeReservas())
            {
                
                string[] campos = linea.Split(";");
                int id_reserva = int.Parse(campos[0]);
                if (id_reserva  == reserva.Id)
                {
                    string ? aux;
                    aux = $"{id_reserva};{reserva.IdPersona};{reserva.IdEventoDeportivo};{reserva.FechaAltaReserva}";
                    lineas.Add(aux);
                }
                else
                {
                    lineas.Add(linea);
                }
            }
            File.WriteAllLines("reservas.csv", lineas);
        }

        public void Eliminar(int id)
        {
            List<string> lineas = new List<string>();
            lineas.Add("id;idPersona;idEvento;fechaReserva");
            foreach (var linea in this.LeerLineasDeReservas())
            {
                string[] campos = linea.Split(";");
                int id_reserva = int.Parse(campos[0]);
                if (id_reserva != id)
                {
                    string ? aux; 
                    int id_persona = int.Parse(campos[1]);
                    int id_evento = int.Parse(campos[2]);
                    DateTime fechaAlta = DateTime.Parse(campos[3]);
                    aux = $"{id_reserva};{id_persona};{id_evento};{fechaAlta}";
                    lineas.Add(aux);
                }
            }
            File.WriteAllLines("reservas.csv", lineas);
        }

        public Reserva? ObtenerPorId(int id)
        {
            foreach (var linea in this.LeerLineasDeReservas())
            {
                string[] campos = linea.Split(';');
                if (campos.Length != 4) continue;
                int id_reserva = int.Parse(campos[0]);
                if (id_reserva == id)
                {
                    int id_persona = int.Parse(campos[1]);
                    int id_evento = int.Parse(campos[2]);
                    DateTime fechaAlta = DateTime.Parse(campos[3]);
                    var reserva = new Reserva(id_reserva, id_persona, id_evento, fechaAlta);
                    return reserva;
                }
            }
            return null;
        }

        public List<Reserva> ListarTodas()
        {
            List<Reserva> lineas = new List<Reserva>();
            foreach (var linea in LeerLineasDeReservas())
            {
                string[] campos = linea.Split(";");
                int id = int.Parse(campos[0]);
                int id_persona = int.Parse(campos[1]);
                int id_evento = int.Parse(campos[2]);
                DateTime fechaAlta = DateTime.Parse(campos[3]);
                var reserva = new Reserva(id, id_persona, id_evento, fechaAlta);
                lineas.Add(reserva);
            }
            return lineas;
        }
        public bool ExisteReserva(int idReserva)
        {
            if (this.ObtenerPorId(idReserva) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ExisteReservaDuplicada(int idPersona, int idEventoDeportivo)
        {
            foreach (var linea in LeerLineasDeReservas())
            {
                string[] campos = linea.Split(";");
                int id_persona = int.Parse(campos[1]);
                int id_evento = int.Parse(campos[2]);
                if ((id_persona == idPersona) && (id_evento == idEventoDeportivo))
                {
                    return true;
                }
            }
            return false;
        }

        public int ContarReservaParaEvento(int idEventoDeportivo)
        {
            int contador = 0;
            foreach (var linea in LeerLineasDeReservas())
            {
                string[] campos = linea.Split(";");
                int id_evento = int.Parse(campos[2]);
                if ((id_evento == idEventoDeportivo))
                    contador++;
            }
            return contador;
        }

        private List<string> LeerLineasDeReservas()
        {
            List<string> lineas = new List<string>();
            if (System.IO.File.Exists("reservas.csv"))
            {
                using var sr = new StreamReader("reservas.csv");
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    var linea = sr.ReadLine();
                    if (!string.IsNullOrWhiteSpace(linea))
                        lineas.Add(linea);
                }
            }
            return lineas;
        }
    }
}