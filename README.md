# CentroEventos 🏟️ – Sistema de Gestión de Eventos Deportivos

Proyecto desarrollado en .NET 8 con arquitectura limpia, para la gestión de eventos deportivos, inscripciones (reservas), usuarios y control de asistencia en un centro universitario.

---

## 🧩 Arquitectura utilizada

Se sigue el modelo de **Arquitectura Limpia (Clean Architecture)**, dividiendo responsabilidades en capas bien separadas:

CentroEventos/
├── CentroEventos.Aplicacion # Lógica de negocio pura
├── CentroEventos.Repositorios # Persistencia (archivos de texto)
└── CentroEventos.Consola # Interfaz de pruebas por consola


---

## 👨‍💻 Integrantes y responsabilidades

| Nombre             | Rol principal                             |
|--------------------|-------------------------------------------|
| Santiago Cáceres   | Casos de uso y lógica de negocio (`Aplicacion`) |
| Luciano Galluzi    | Repositorios de archivos (`Repositorios`) |
| Mateo Negri        | Consola de pruebas y documentación         |
| Sebastian Basmachi | Autorización, permisos y coordinación técnica |

---

## 📦 Funcionalidades del sistema

- **Personas**: alta, modificación, eliminación, listado
- **Eventos deportivos**: alta, modificación (si no ocurrió), eliminación (si no tiene reservas), listado
- **Reservas**: alta (con validación de cupo), modificación, eliminación, listado
- **Consultas especiales**:
  - Eventos con cupo disponible
  - Asistencia a eventos pasados

---

## 🔐 Control de permisos

El sistema controla el acceso a las operaciones según el permiso del usuario:

- `Permiso.UsuarioAlta`, `Permiso.EventoBaja`, etc.
- Servicio `IServicioAutorizacion` con implementación provisoria

---

## ⚠️ Reglas de negocio implementadas

- No se puede reservar si no hay cupo
- No se puede eliminar una persona con reservas o que sea responsable
- No se puede modificar un evento pasado
- No se puede eliminar un evento con reservas
- Validaciones de campos vacíos, fechas inválidas, duplicados

---

## 🧪 Cómo probarlo

- Ejecutar el proyecto `CentroEventos.Consola`
- Se pueden cargar ejemplos de prueba y verificar los casos de uso manualmente
- Las excepciones muestran mensajes claros por consola

---

## 🧠 Tecnologías utilizadas

- .NET 8
- C#
- Arquitectura Limpia
- Git + GitHub
- Archivos de texto plano como persistencia

---

## 📁 Organización interna

CentroEventos/
├── .sln (solución)
├── CentroEventos.Aplicacion/ # Entidades, casos de uso, validadores, excepciones
├── CentroEventos.Repositorios/ # Implementaciones de persistencia
└── CentroEventos.Consola/ # Entrada de pruebas (Program.cs)


---

## 📄 Licencia

Proyecto académico – Seminario de lenguajes .NET - Facultad de Informática, UNLP – Año 2025
