# CentroEventos – Lógica de Aplicación (.NET 8)

Este proyecto implementa la capa de lógica de negocio para un sistema de gestión de eventos deportivos en un centro universitario, siguiendo los principios de **arquitectura limpia (clean architecture)**.

---

## 🎯 Objetivo del sistema

Gestionar personas, eventos deportivos, reservas e historial de asistencia. Permitir acciones CRUD y aplicar reglas de negocio como control de cupo, validación de duplicados y autorizaciones.

---

## 👤 Integrante responsable de esta capa

- **Santiago Cáceres**
  - Desarrollo de la capa `CentroEventos.Aplicacion`
  - Casos de uso
  - Validaciones
  - Excepciones personalizadas
  - Interfaces de repositorios
  - Control de permisos

---

## 🧱 Estructura del proyecto Aplicacion

CentroEventos.Aplicacion/
├── CasosDeUso/
│ ├── Persona/
│ ├── EventoDeportivoU/
│ └── ReservaU/
├── Entidades/
├── Excepciones/
├── Repositorios/
├── Servicios/
└── Validadores/

---

## 📦 Funcionalidades implementadas

### ✳️ Entidades

- `Persona`, `EventoDeportivo`, `Reserva` + `EstadoAsistencia`

### ✅ Validadores

- Reglas específicas para cada entidad (campos obligatorios, fechas, cupos, existencia, duplicados)

### 🔐 Permisos y servicios

- Enum `Permiso`
- Interfaz `IServicioAutorizacion`
- Excepciones como `FalloAutorizacionException`

### 💾 Interfaces de repositorio

- `IRepositorioPersona`, `IRepositorioEventoDeportivo`, `IRepositorioReserva`

### 🧠 Casos de uso implementados

#### Persona
- Alta, Modificación, Eliminación, Listado

#### EventoDeportivo
- Alta, Modificación, Eliminación, Listado
- Casos especiales:
  - Listar eventos con cupo disponible
  - Listar asistencia a evento finalizado

#### Reserva
- Alta, Modificación, Eliminación, Listado

---

## ❗ Excepciones personalizadas

- `ValidacionException`
- `DuplicadoException`
- `EntidadNotFoundException`
- `CupoExcedidoException`
- `OperacionInvalidaException`
- `FalloAutorizacionException`

---

## 🔗 Dependencias entre capas

| Capa                  | Puede referenciar a...        |
|-----------------------|-------------------------------|
| Aplicacion (este)     | ❌ A ninguna otra             |
| Repositorios          | ✅ Aplicacion                 |
| Consola               | ✅ Aplicacion, Repositorios   |

---

## 🛠 Tecnologías utilizadas

- C#
- .NET 8
- Clean Architecture
- Git + GitHub

---

## 📄 Licencia

Proyecto académico – Seminario de lenguajes .NET - Facultad de Informática, UNLP – Año 2025
