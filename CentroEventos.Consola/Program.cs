using CentroEventos.Aplicacion.Entidades; 
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Servicios;
using CentroEventos.Aplicacion.CasosDeUso.PersonaU;
using CentroEventos.Aplicacion.CasosDeUso.EventoDeportivoU;
using CentroEventos.Aplicacion.CasosDeUso.ReservaU;

var repoPersonas = new RepositorioPersonaTXT("personas.txt");
var repoEventos = new RepositorioEventoDeportivoTXT("eventos.txt");
var repoReservas = new RepositorioReservaTXT("reservas.txt"); 
IServicioAutorizacion servicioAuth = new ServicioAutorizacionProvisorio();

int idAdmin = 1;
bool continuar = true;
while (continuar)
{

    Console.WriteLine("=== Centro de Eventos Deportivos ===");
    Console.WriteLine("1. Alta de Persona");
    Console.WriteLine("2. Alta de Evento Deportivo");
    Console.WriteLine("3. Alta de Reserva");
    Console.WriteLine("4. Listar eventos con cupo disponible");
    Console.WriteLine("5. Listar asistencia a evento");
    Console.WriteLine("6. Baja de Persona");
    Console.WriteLine("7. Baja de Evento Deportivo");
    Console.WriteLine("8. Baja de Reserva");
    Console.WriteLine("9. Modificar Persona");
    Console.WriteLine("10. Modificar Evento Deportivo");
    Console.WriteLine("11. Modificar Reserva");
    Console.WriteLine("0. Salir");
    Console.WriteLine("Seleccione una opción: ");

    var opcion = Console.ReadLine();


    try
    {
        switch (opcion)
        {
            case "1":
                var persona = new Persona("12345678", "Mateo", "Negri", "mateo@mail.com", "2211234567");
                var altaPersona = new AltaPersonaUseCase(repoPersonas, servicioAuth);
                altaPersona.Ejecutar(persona, idAdmin);
                Console.WriteLine("Persona agregada correctamente.");

                Console.WriteLine("Presione cualquier tecla para continuar.....");
                Console.ReadKey();
                break;

            case "2":
                var evento = new EventoDeportivo("Fútbol 5", "Partido amistoso", DateTime.Now.AddDays(1), 1.5, 10, 1);
                var altaEvento = new AltaEventoDeportivoUseCase(repoEventos, repoPersonas, servicioAuth);
                altaEvento.Ejecutar(evento, idAdmin);
                Console.WriteLine("Evento creado correctamente.");

                Console.WriteLine("Presione cualquier tecla para continuar.....");
                Console.ReadKey();
                break;

            case "3":
                var reserva = new Reserva(1, 1, DateTime.Now);
                var altaReserva = new AltaReservaUseCase(repoReservas, repoEventos, repoPersonas, servicioAuth);
                altaReserva.Ejecutar(reserva, idAdmin);
                Console.WriteLine("Reserva realizada exitosamente.");

                Console.WriteLine("Presione cualquier tecla para continuar.....");
                Console.ReadKey();
                break;

            case "4":
                var listarEventos = new ListarEventosConCupoDisponibleUseCase(repoEventos, repoReservas);
                var disponibles = listarEventos.Ejecutar();
                Console.WriteLine("Eventos con cupo disponible:");
                foreach (var ev in disponibles)
                {
                    Console.WriteLine(ev);
                }

                Console.WriteLine("Presione cualquier tecla para continuar.....");
                Console.ReadKey();
                break;

            case "5":
                Console.Write("Ingrese ID del evento: ");
                int idEvento = int.Parse(Console.ReadLine() ?? "0");
                var listarAsistencia = new ListarAsistenciaAEventoUseCase(repoEventos, repoReservas);
                var asistencia = listarAsistencia.Ejecutar(idEvento);
                Console.WriteLine("Asistencias registradas en el evento:");
                foreach (var reser in asistencia)
                {
                    Console.WriteLine($"Persona ID: {reser.IdPersona}, Asistencia: {reser.EstadoAsistencia}");
                }

                Console.WriteLine("Presione cualquier tecla para continuar.....");
                Console.ReadKey();
                break;
            case "6":
                Console.Write("Ingrese ID de la persona a eliminar: ");
                string ? idbaja = Console.ReadLine();
                if (int.TryParse(idbaja, out int id))
                {
                    // id contiene el valor convertido
                }
                else
                {
                    Console.WriteLine("El valor ingresado no es un número válido.");
                }
                var bajaPersona = new EliminarPersonaUseCase(repoPersonas,repoEventos,repoReservas, servicioAuth);
                bajaPersona.Ejecutar(id, idAdmin);
                Console.WriteLine("Persona eliminada correctamente.");
                break;

            case "7":
                Console.Write("Ingrese ID del evento a eliminar: ");
                int idEventoBaja = int.Parse(Console.ReadLine() ?? "0");
                var bajaEvento = new EliminarEventoDeportivoUseCase(repoEventos,repoReservas, servicioAuth);
                bajaEvento.Ejecutar(idEventoBaja, idAdmin);
                Console.WriteLine("Evento eliminado correctamente.");
                break;

            case "8":
                Console.Write("Ingrese ID de la reserva a eliminar: ");
                int idReservaBaja = int.Parse(Console.ReadLine() ?? "0");
                var bajaReserva = new EliminarReservaUseCase(repoReservas, servicioAuth);
                bajaReserva.Ejecutar(idReservaBaja, idAdmin);
                Console.WriteLine("Reserva eliminada correctamente.");
                break;
            case "9":
                Console.Write("Ingrese DNI de la persona a modificar: ");
                string? dniMod = Console.ReadLine();
                while (dniMod == null)
                {
                   Console.Write("Ingrese DNI de la persona a modificar: ");
                   dniMod = Console.ReadLine(); 
                }
            string ? nuevoNombre = "Pepe";
            string? nuevoApellido = "Argento";
            string? nuevoEmail = "Pargento@mail.com";
            string? nuevoTelefono = "22133423";
            var personaMod = new Persona(dniMod, nuevoNombre,nuevoApellido,nuevoEmail,nuevoTelefono);
            var modificarPersona = new ModificarPersonaUseCase(repoPersonas, servicioAuth);
            modificarPersona.Ejecutar(personaMod, idAdmin);
            Console.WriteLine("Persona modificada correctamente.");
            break;

        case "10":
            Console.Write("Ingrese ID del evento a modificar: ");
            int idEventoMod = int.Parse(Console.ReadLine() ?? "0");
            var eventoMod = new EventoDeportivo("Nuevo Evento", "Descripción actualizada", DateTime.Now.AddDays(2), 2.0, 15, 1);
            eventoMod.AsignarId(idEventoMod);
            var modificarEvento = new ModificarEventoDeportivoUseCase(repoEventos,repoPersonas, servicioAuth);
            modificarEvento.Ejecutar(eventoMod, idAdmin);
            Console.WriteLine("Evento modificado correctamente.");
            break;

        case "11":
            Console.Write("Ingrese ID de la reserva a modificar: ");
            int idReservaMod = int.Parse(Console.ReadLine() ?? "0");
                DateTime fecha = DateTime.Now;
            var reservaMod = new Reserva(1, 1,fecha);
            reservaMod.AsignarId(idReservaMod);
            var modificarReserva = new ModificarReservaUseCase(repoReservas, servicioAuth);
            modificarReserva.Ejecutar(reservaMod, idAdmin);
            Console.WriteLine("Reserva modificada correctamente.");
            break;


        case "0":
            Console.WriteLine("Saliendo del programa.....");
            continuar = false;
            break;


            default:
                Console.WriteLine("Opción no válida");
                break;
        }

    }
    catch (ValidacionException ex)
    { Console.WriteLine("Error de validación: " + ex.Message); }
    catch (DuplicadoException ex)
    { Console.WriteLine("Error: Ya existe un elemento con esos datos. " + ex.Message); }
    catch (EntidadNotFoundException ex)
    { Console.WriteLine("Error: No se encontró la entidad solicitada. " + ex.Message); }
    catch (OperacionInvalidaException ex)
    { Console.WriteLine("Error: Operación no permitida. " + ex.Message); }
    catch (CupoExcedidoException ex)
    { Console.WriteLine("Error: No hay cupo disponible para este evento. " + ex.Message); }
    catch (FalloAutorizacionException ex)
    { Console.WriteLine("Error: No tenés permisos para realizar esta acción. " + ex.Message); }
    catch (Exception ex) { Console.WriteLine("Error inesperado: " + ex.Message); }
}