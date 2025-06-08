
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Servicios;
using CentroEventos.Aplicacion.CasosDeUso.PersonaU;
using CentroEventos.Aplicacion.CasosDeUso.EventoDeportivoU; 
using CentroEventos.Aplicacion.CasosDeUso.ReservaU;
using CentroEventos.Aplicacion;
using CentroEventos.Repositorios.Repositorios;



var repoPersonas =  new RepositorioPersona ();
var repoEventos = new RepositorioEventoDeportivo();
var repoReservas = new RepositorioReserva(); 
IServicioAutorizacion servicioAuth = new ServicioAutorizacionProvisorio();

int idAdmin = 1;
bool continuar = true; 
while (continuar) {
    Console.WriteLine("=== Centro de Eventos Deportivos ==="); 
    Console.WriteLine("1. Altas de Personas.");
    Console.WriteLine("2. Altas de Eventos.");
    Console.WriteLine("3. Altas de Reservas.");
    Console.WriteLine("4. Listar eventos con cupo disponible");
    Console.WriteLine("5. Listar asistencia a evento");
    Console.WriteLine("6. Bajas de Personas.");
    Console.WriteLine("7. Bajas de Eventos.");
    Console.WriteLine("8. Bajas de Reservas.");
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
                var altaPersona = new AltaPersonaUseCase(repoPersonas, servicioAuth);
                altaPersona.Ejecutar(new Persona("12345678", "Mateo", "Negri", "mateo@mail.com", "2211234567"), idAdmin);
                altaPersona.Ejecutar(new Persona("23456789", "Lucía", "Pérez", "lucia@mail.com", "2217654321"), idAdmin);
                altaPersona.Ejecutar(new Persona("34567890", "Juan", "Gómez", "juan@mail.com", "221998877"), idAdmin);
                Console.WriteLine("3 personas agregadas correctamente.");
            break;

            case "2":
                var altaEvento = new AltaEventoDeportivoUseCase(repoEventos, repoPersonas, servicioAuth);
                altaEvento.Ejecutar(new EventoDeportivo("Fútbol 5", "Amistoso", DateTime.Now.AddDays(1), 1.5, 10, 1), idAdmin);
                altaEvento.Ejecutar(new EventoDeportivo("Tenis", "Torneo local", DateTime.Now.AddDays(3), 2, 5, 1), idAdmin);
                altaEvento.Ejecutar(new EventoDeportivo("Basket", "Entrenamiento", DateTime.Now.AddDays(2), 1, 12, 2), idAdmin);
                Console.WriteLine("3 eventos creados correctamente.");
                break;

            case "3":
                var altaReserva = new AltaReservaUseCase(repoReservas, repoEventos, repoPersonas, servicioAuth);
                altaReserva.Ejecutar(new Reserva(1, 1, DateTime.Now), idAdmin);
                altaReserva.Ejecutar(new Reserva(2, 1, DateTime.Now), idAdmin);
                altaReserva.Ejecutar(new Reserva(3, 2, DateTime.Now), idAdmin);
                Console.WriteLine("3 reservas realizadas exitosamente.");
                break;

            case "4":
                var listarEventos = new ListarEventosConCupoDisponibleUseCase(repoEventos, repoReservas);
                var disponibles = listarEventos.Ejecutar();
                Console.WriteLine("Eventos con cupo disponible:");
                foreach (var ev in disponibles)
                {
                    Console.WriteLine(ev);
                }
                break;

            case "5":
                Console.Write("Ingrese ID del evento: ");
                int idEvento = int.Parse(Console.ReadLine() ?? "0");
                var listarAsistencia = new ListarAsistenciaAEventoUseCase(repoEventos, repoReservas);
                var asistencia = listarAsistencia.Ejecutar(idEvento);
                foreach (var r in asistencia)
                {
                    Console.WriteLine($"Persona ID: {r.IdPersona}, Asistencia: {r.EstadoAsistencia}");
                }
                break;

            case "6":
                var bajaPersona = new EliminarPersonaUseCase(repoPersonas, repoEventos, repoReservas, servicioAuth);
                bajaPersona.Ejecutar(1, idAdmin);
                bajaPersona.Ejecutar(2, idAdmin);
                Console.WriteLine("2 personas eliminadas correctamente.");
                break;

            case "7":
                var bajaEvento = new EliminarEventoDeportivoUseCase(repoEventos, repoReservas, servicioAuth);
                bajaEvento.Ejecutar(1, idAdmin);
                bajaEvento.Ejecutar(2, idAdmin);
                Console.WriteLine("2 eventos eliminados correctamente.");
                break;

            case "8":
                var bajaReserva = new EliminarReservaUseCase(repoReservas, servicioAuth);
                bajaReserva.Ejecutar(1, idAdmin);
                bajaReserva.Ejecutar(2, idAdmin);
                Console.WriteLine("2 reservas eliminadas correctamente.");
                break;

            case "9":
                var modificarPersona = new ModificarPersonaUseCase(repoPersonas, servicioAuth);
                var personaMod = new Persona("34567890", "Juan Manuel", "Gómez", "juan.actualizado@mail.com", "221334455");
                modificarPersona.Ejecutar(personaMod, idAdmin);
                Console.WriteLine("Persona modificada correctamente.");
                break;

            case "10":
                var modificarEvento = new ModificarEventoDeportivoUseCase(repoEventos, repoPersonas, servicioAuth);
                var eventoMod = new EventoDeportivo("Tenis Avanzado", "Torneo regional", DateTime.Now.AddDays(4), 2.5, 8, 1);
                eventoMod.AsignarId(2);
                modificarEvento.Ejecutar(eventoMod, idAdmin);
                Console.WriteLine("Evento modificado correctamente.");
                break;

            case "11":
                var modificarReserva = new ModificarReservaUseCase(repoReservas, servicioAuth);
                var reservaMod = new Reserva(3, 2, DateTime.Now);
                reservaMod.AsignarId(3);
                modificarReserva.Ejecutar(reservaMod, idAdmin);
                Console.WriteLine("Reserva modificada correctamente.");
                break;

            case "0":
                continuar = false;
                Console.WriteLine("Saliendo del programa...");
                break;

            default:
                Console.WriteLine("Opción no válida");
                break;
        }
    }
    catch (ValidacionException ex) { Console.WriteLine("Error de validación: " + ex.Message); }
    catch (DuplicadoException ex) { Console.WriteLine("Error: Ya existe un elemento con esos datos. " + ex.Message); }
    catch (EntidadNotFoundException ex) { Console.WriteLine("Error: No se encontró la entidad solicitada. " + ex.Message); }
    catch (OperacionInvalidaException ex) { Console.WriteLine("Error: Operación no permitida. " + ex.Message); }
    catch (CupoExcedidoException ex) { Console.WriteLine("Error: No hay cupo disponible para este evento. " + ex.Message); }
    catch (FalloAutorizacionException ex) { Console.WriteLine("Error: No tenés permisos para realizar esta acción. " + ex.Message); }
    catch (Exception ex) { Console.WriteLine("Error inesperado: " + ex.Message); }
}