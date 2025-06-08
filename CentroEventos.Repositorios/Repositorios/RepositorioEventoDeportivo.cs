
using System.IO;
using System.Collections.Generic;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Repositorios;

namespace CentroEventos.Repositorios.Repositorios
{
    public class RepositorioEventoDeportivo : IRepositorioEventoDeportivo
    {
        private const string ArchivoEventos = "eventos.csv";
        private const string ArchivoId = "eventos_ID.csv";

        public RepositorioEventoDeportivo()
        {
            if (!File.Exists(ArchivoEventos))
                File.WriteAllText(ArchivoEventos, "id;nombre;descripcion;fechaInicio;duracionHoras;cupoMaximo;idResponsable\n");

            if (!File.Exists(ArchivoId))
                File.WriteAllText(ArchivoId, "1");
        }

        public void Agregar(EventoDeportivo evento)
        {
            int id = int.Parse(File.ReadAllText(ArchivoId));
            File.WriteAllText(ArchivoId, (id + 1).ToString());

            string linea = $"{id};{evento.Nombre};{evento.Descripcion};{evento.FechaInicio};{evento.DuracionHoras};{evento.CupoMaximo};{evento.IdResponsable}";
            File.AppendAllText(ArchivoEventos, linea + "\n");
        }

        public void Modificar(EventoDeportivo evento)
        {
            var nuevasLineas = new List<string> { "id;nombre;descripcion;fechaInicio;duracionHoras;cupoMaximo;idResponsable" };

            foreach (var linea in LeerLineasDeEventos())
            {
                var campos = linea.Split(";");
                int id = int.Parse(campos[0]);
                if (id == evento.Id)
                {
                    string modificado = $"{evento.Id};{evento.Nombre};{evento.Descripcion};{evento.FechaInicio};{evento.DuracionHoras};{evento.CupoMaximo};{evento.IdResponsable}";
                    nuevasLineas.Add(modificado);
                }
                else
                {
                    nuevasLineas.Add(linea);
                }
            }

            File.WriteAllLines(ArchivoEventos, nuevasLineas);
        }

        public void Eliminar(int id)
        {
            var nuevasLineas = new List<string> { "id;nombre;descripcion;fechaInicio;duracionHoras;cupoMaximo;idResponsable" };

            foreach (var linea in LeerLineasDeEventos())
            {
                var campos = linea.Split(";");
                if (int.Parse(campos[0]) != id)
                    nuevasLineas.Add(linea);
            }

            File.WriteAllLines(ArchivoEventos, nuevasLineas);
        }

        public EventoDeportivo? ObtenerPorId(int id)
        {
            foreach (var linea in LeerLineasDeEventos())
            {
                var campos = linea.Split(";");
                if (int.Parse(campos[0]) == id)
                {
                    return new EventoDeportivo(id, campos[1], campos[2], DateTime.Parse(campos[3]),
                        double.Parse(campos[4]), int.Parse(campos[5]), int.Parse(campos[6]));
                }
            }
            return null;
        }

        public List<EventoDeportivo> ListarTodos()
        {
            var lista = new List<EventoDeportivo>();
            foreach (var linea in LeerLineasDeEventos())
            {
                var campos = linea.Split(";");
                lista.Add(new EventoDeportivo(int.Parse(campos[0]), campos[1], campos[2], DateTime.Parse(campos[3]),
                    double.Parse(campos[4]), int.Parse(campos[5]), int.Parse(campos[6])));
            }
            return lista;
        }

        public bool ExisteId(int id)
        {
            return ObtenerPorId(id) != null;
        }

        public bool HayCupoDisponible(int id)
        {
            var evento = ObtenerPorId(id);
            if (evento == null) return false;

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
                        if (int.Parse(campos[2]) == id)
                            contador++;
                    }
                }
            }

            return contador < evento.CupoMaximo;
        }

        private List<string> LeerLineasDeEventos()
        {
            var lineas = new List<string>();
            if (!File.Exists(ArchivoEventos)) return lineas;

            using var sr = new StreamReader(ArchivoEventos);
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
