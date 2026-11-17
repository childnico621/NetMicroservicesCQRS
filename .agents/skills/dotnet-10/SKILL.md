---
name: dotnet-10
description: >-
  Guía para desarrollar y migrar proyectos de .NET 8 a .NET 10 (C# 14, ASP.NET Core, EF Core,
  Dockerfiles y CI/CD) en el repositorio NetMicroservicesCQRS. Úsala al actualizar TargetFramework,
  paquetes NuGet, imágenes Docker, workflows de GitHub Actions o resolver breaking changes de .NET 10.
---

# Trabajo y Migración a .NET 10

Esta skill guía el desarrollo en **.NET 10** y la migración desde **.NET 8** en este repositorio (`NetMicroservicesCQRS`).

## Checklist de migración (.NET 8 → .NET 10)

### 1. SDK y `global.json`
- Verificar que el SDK de .NET 10 esté instalado (`dotnet --list-sdks`).
- Si existe `global.json`, actualizar `"version"` a la versión `10.0.x` correspondiente.

### 2. Archivos de proyecto (`.csproj` / `Directory.Build.props`)
- Actualizar `<TargetFramework>net8.0</TargetFramework>` a:
  ```xml
  <TargetFramework>net10.0</TargetFramework>
  ```
- Si se usa `Directory.Build.props`, centralizar ahí `TargetFramework` y `LangVersion` (`14` o `latest`) cuando aplique.

### 3. Paquetes NuGet
- Actualizar los paquetes oficiales de Microsoft a sus versiones `10.0.*` (por ejemplo: `Microsoft.AspNetCore.*`, `Microsoft.EntityFrameworkCore.*`, `Microsoft.Extensions.*`).
- Revisar compatibilidad de paquetes de terceros (MediatR, AutoMapper, FluentValidation, MassTransit, Swashbuckle/OpenAPI, drivers de SQL Server, PostgreSQL/Npgsql, MySQL/Pomelo, etc.) antes de subir versiones mayores.
- Ejecutar `dotnet restore` y revisar advertencias de compatibilidad o deprecaciones (`NU1605`, `SYSLIB*`, `ASPDEPR*`).

### 4. Código y Breaking Changes
- Priorizar las APIs y convenciones modernas de .NET 10 / C# 14.
- Revisar cambios de comportamiento entre .NET 8 y .NET 10 en:
  - **ASP.NET Core**: OpenAPI integrado (`Microsoft.AspNetCore.OpenApi`), Minimal APIs, autenticación/autorización y serialización JSON (`System.Text.Json`).
  - **Entity Framework Core 10**: cambios en consultas LINQ, migraciones y proveedores de base de datos.
  - **Seguridad y criptografía**: algoritmos o configuraciones obsoletas marcadas con `SYSLIB*`.

### 5. Contenedores (Docker) y CI/CD
- Actualizar las imágenes base en los `Dockerfile`s:
  - Build: `mcr.microsoft.com/dotnet/sdk:10.0`
  - Runtime: `mcr.microsoft.com/dotnet/aspnet:10.0`
- Actualizar los workflows de GitHub Actions (`.github/workflows/*.yml`) para usar `dotnet-version: '10.0.x'` en `actions/setup-dotnet`.

### 6. Verificación
Después de cada cambio de proyecto o paquete:
```bash
dotnet restore
dotnet build --no-restore
dotnet test --no-build
```
Si hay Dockerfiles, validar también la construcción de la imagen (`docker build`) antes de abrir el Pull Request.
