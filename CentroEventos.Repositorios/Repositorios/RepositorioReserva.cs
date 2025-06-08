
using System.IO;
using System.Collections.Generic;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Repositorios;

namespace CentroEventos.Repositorios.Repositorios
{
    public class RepositorioReserva : IRepositorioReserva
    {
        private const string ArchivoReservas = "reservas.csv";
        private const string ArchivoId = "reservas_ID.csv";

        public RepositorioReserva()
        {
            if (!File.Exists(ArchivoReservas))
                File.WriteAllText(ArchivoReservas, "id;idPersona;idEvento;fechaReserva\n");

            if (!File.Exists(ArchivoId))
                File.WriteAllText(ArchivoId, "1");
        }

        public void Agregar(Reserva reserva)
        {
            int id = int.Parse(File.ReadAllText(ArchivoId));
            File.WriteAllText(ArchivoId, (id + 1).ToString());
            reserva.AsignarId(id);

            string linea = $"{reserva.Id};{reserva.IdPersona};{reserva.IdEventoDeportivo};{reserva.FechaAltaReserva}";
            File.AppendAllText(ArchivoReservas, linea + "\n");
        }

        public void Modificar(Reserva reserva)
        {
            var nuevasLineas = new List<string> { "id;idPersona;idEvento;fechaReserva" };

            foreach (var linea in LeerLineasDeReservas())
            {
                var campos = linea.Split(";");
                int id_reserva = int.Parse(campos[0]);
                if (id_reserva == reserva.Id)
                {
                    string nueva = $"{reserva.Id};{reserva.IdPersona};{reserva.IdEventoDeportivo};{reserva.FechaAltaReserva}";
                    nuevasLineas.Add(nueva);
                }
                else
                {
                    nuevasLineas.Add(linea);
                }
            }

            File.WriteAllLines(ArchivoReservas, nuevasLineas);
        }

        public void Eliminar(int id)
        {
            var nuevasLineas = new List<string> { "id;idPersona;idEvento;fechaReserva" };

            foreach (var linea in LeerLineasDeReservas())
            {
                var campos = linea.Split(";");
                if (int.Parse(campos[0]) != id)
                    nuevasLineas.Add(linea);
            }

            File.WriteAllLines(ArchivoReservas, nuevasLineas);
        }

        public Reserva? ObtenerPorId(int id)
        {
            foreach (var linea in LeerLineasDeReservas())
            {
                var campos = linea.Split(";");
                if (int.Parse(campos[0]) == id)
                {
                    return new Reserva(id, int.Parse(campos[1]), int.Parse(campos[2]), DateTime.Parse(campos[3]));
                }
            }
            return null;
        }

        public List<Reserva> ListarTodas()
        {
            var lista = new List<Reserva>();
            foreach (var linea in LeerLineasDeReservas())
            {
                var campos = linea.Split(";");
                lista.Add(new Reserva(int.Parse(campos[0]), int.Parse(campos[1]), int.Parse(campos[2]), DateTime.Parse(campos[3])));
            }
            return lista;
        }

        public bool ExisteReserva(int idReserva)
        {
            return ObtenerPorId(idReserva) != null;
        }

        public bool ExisteReservaDuplicada(int idPersona, int idEventoDeportivo)
        {
            foreach (var linea in LeerLineasDeReservas())
            {
                var campos = linea.Split(";");
                if (int.Parse(campos[1]) == idPersona && int.Parse(campos[2]) == idEventoDeportivo)
                    return true;
            }
            return false;
        }

        public int ContarReservaParaEvento(int idEventoDeportivo)
        {
            int contador = 0;
            foreach (var linea in LeerLineasDeReservas())
            {
                var campos = linea.Split(";");
                if (int.Parse(campos[2]) == idEventoDeportivo)
                    contador++;
            }
            return contador;
        }

        private List<string> LeerLineasDeReservas()
        {
            var lineas = new List<string>();
            if (!File.Exists(ArchivoReservas)) return lineas;

            using var sr = new StreamReader(ArchivoReservas);
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
