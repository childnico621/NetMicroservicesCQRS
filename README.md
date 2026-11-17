# NetMicroservicesCQRS

Proyecto práctico de arquitectura de microservicios en **.NET 10** aplicando el patrón **CQRS** (Command Query Responsibility Segregation), comunicación asíncrona basada en eventos con **RabbitMQ**, enrutamiento mediante **API Gateway (Ocelot)** y persistencia políglota.

---

## Arquitectura y Tecnologías

- **Framework:** .NET 10 (`net10.0`), ASP.NET Core Web API
- **Patrón CQRS & Mediador:** MediatR
- **Mapeo de objetos:** AutoMapper
- **Validaciones:** FluentValidation
- **API Gateway:** Ocelot
- **Mensajería / Event Bus:** RabbitMQ (`RabbitMQ.Client`)
- **Notificaciones:** SendGrid
- **Persistencia (Entity Framework Core 10):**
  - **SQL Server** (`Microsoft.EntityFrameworkCore.SqlServer`) para el servicio de Libros
  - **PostgreSQL** (`Npgsql.EntityFrameworkCore.PostgreSQL`) para el servicio de Autores
  - **MySQL** (`MySql.EntityFrameworkCore`) para el servicio de Carrito de Compras
- **Documentación API:** Swagger / Swashbuckle (`Swashbuckle.AspNetCore`)
- **Pruebas Unitarias:** xUnit, Moq, GenFu, EF Core InMemory
- **Contenedores:** Docker (`Dockerfile` por microservicio)

---

## Estructura de la Solución (`TiendaServicios.sln`)

| Proyecto | Tipo | Descripción | Base de Datos |
| :--- | :--- | :--- | :--- |
| **`TiendaServicios.Api.Autor`** | Web API | Gestión de autores y grados académicos. Consume eventos de RabbitMQ (`EmailEventHandler`). | PostgreSQL |
| **`TiendaServicios.Api.Book`** | Web API | Gestión de libros/materiales bibliográficos con CQRS y publicación de eventos. | SQL Server |
| **`TiendaServicios.Api.ShoppingCart`** | Web API | Gestión de sesiones y detalles del carrito de compras. Se comunica de forma remota con `Api.Book`. | MySQL |
| **`TiendaServicios.Api.Gateway`** | Web API (Gateway) | Puerta de enlace con **Ocelot** (`ocelot.json`) y agregación/delegación de respuestas (`BookHandler`, `RemoteAuthor`). | N/A |
| **`TiendaServicios.RabbitMQ.Bus`** | Class Library | Implementación del bus de eventos (`RabbitEventBus`), comandos, eventos e interfaces (`IRabbitEventBus`, `IEventHandler`). | N/A |
| **`TiendaServicios.Messenger.Email`** | Class Library | Servicio de envío de correos electrónicos integrado con **SendGrid**. | N/A |
| **`TiendaServicios.Api.Test.Books`** | Test Project (xUnit) | Pruebas unitarias para `TiendaServicios.Api.Book` y `TiendaServicios.RabbitMQ.Bus` usando `Moq`, `GenFu` y EF Core InMemory. | InMemory |

---

## Requisitos Previos

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- Docker (opcional, para ejecutar los contenedores o bases de datos/RabbitMQ)
- Instancias de **SQL Server**, **PostgreSQL**, **MySQL** y **RabbitMQ** configuradas según los `appsettings.json` de cada microservicio.

---

## Cómo Empezar

### 1. Restaurar dependencias y compilar

```bash
dotnet restore TiendaServicios.sln
dotnet build TiendaServicios.sln
```

### 2. Ejecutar las pruebas unitarias

```bash
dotnet test TiendaServicios.sln
```

### 3. Ejecutar un microservicio localmente

Puedes iniciar cualquiera de las APIs individualmente (por ejemplo, `TiendaServicios.Api.Book`):

```bash
dotnet run --project TiendaServicios.Api.Book/TiendaServicios.Api.Book.csproj
```

Cada servicio expone su interfaz de **Swagger** en entorno de desarrollo para explorar y probar los endpoints.
