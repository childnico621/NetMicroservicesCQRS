---
name: gitflow-github
description: >-
  Guía y flujo de trabajo GitFlow adaptado a GitHub y al repositorio NetMicroservicesCQRS.
  Úsala al crear ramas (feature, release, hotfix), abrir Pull Requests, sincronizar con dev/main,
  o publicar versiones.
---

# GitFlow en GitHub para NetMicroservicesCQRS

Esta skill define el flujo de trabajo GitFlow adaptado a GitHub para este proyecto (`childnico621/NetMicroservicesCQRS`).

## Estructura de ramas del proyecto

- **`main`**: Rama de producción. Solo recibe merges desde `release/*` o `hotfix/*` (vía Pull Request). Cada merge a `main` debe llevar su tag de versión (`vX.Y.Z`).
- **`dev`**: Rama de integración principal (equivalente a `develop` en GitFlow clásico; en este repositorio se llama `dev`). Todas las ramas `feature/*` parten de `dev` y se integran de vuelta en `dev`.
- **`feature/<nombre>`**: Desarrollo de nuevas funcionalidades o cambios por microservicio/módulo (ej. `feature/ms-book-pagination`, `feature/ms-author-validation`).
- **`release/vX.Y.Z`**: Preparación de una nueva versión a partir de `dev` (ajustes finales, versión, changelog, correcciones menores).
- **`hotfix/<nombre>`**: Corrección urgente en producción, creada a partir de `main`.

## Reglas generales

1. **Nunca hacer push directo** a `main` ni a `dev` si hay cambios de funcionalidad; usar siempre **Pull Requests (PR)** en GitHub.
2. Mantener los commits claros y descriptivos (preferiblemente Conventional Commits: `feat:`, `fix:`, `chore:`, `docs:`, `refactor:`, `test:`).
3. Antes de crear una rama nueva, actualizar siempre la rama base (`dev` o `main`).
4. Borrar las ramas `feature/*`, `release/*` y `hotfix/*` tras completar el merge del PR.

---

## Flujos de trabajo

### 1. Iniciar una Feature

Parte desde `dev` actualizada:

```bash
git checkout dev
git pull origin dev
git checkout -b feature/<nombre-corto>
```

Al terminar el desarrollo:

```bash
git push -u origin feature/<nombre-corto>
```

Crear un Pull Request en GitHub hacia **`dev`**:
- Con GitHub CLI:
  ```bash
  gh pr create --base dev --head feature/<nombre-corto> --title "feat: <descripción>" --body "<resumen de cambios>"
  ```
- Una vez aprobado y mergeado en GitHub, limpiar localmente:
  ```bash
  git checkout dev
  git pull origin dev
  git branch -d feature/<nombre-corto>
  ```

### 2. Preparar una Release

Parte desde `dev` cuando esté lista para estabilización:

```bash
git checkout dev
git pull origin dev
git checkout -b release/vX.Y.Z
git push -u origin release/vX.Y.Z
```

Si se hacen correcciones menores en `release/vX.Y.Z`, commitearlas en esa rama. Luego abrir PRs en GitHub:
1. PR de `release/vX.Y.Z` → **`main`** (para publicar).
2. PR de `release/vX.Y.Z` → **`dev`** (para traer de vuelta cualquier ajuste hecho en la release).

Tras el merge a `main`, crear el tag de versión:

```bash
git checkout main
git pull origin main
git tag -a vX.Y.Z -m "Release vX.Y.Z"
git push origin vX.Y.Z
```

### 3. Aplicar un Hotfix

Parte desde `main` para corregir un error crítico en producción:

```bash
git checkout main
git pull origin main
git checkout -b hotfix/<nombre-corto>
```

Tras corregir y probar:

```bash
git push -u origin hotfix/<nombre-corto>
```

Abrir dos PRs en GitHub:
1. PR de `hotfix/<nombre-corto>` → **`main`**
2. PR de `hotfix/<nombre-corto>` → **`dev`** (para no perder el fix en desarrollo)

Tras el merge a `main`, etiquetar la nueva versión patch (`vX.Y.Z+1`) y eliminar la rama `hotfix/<nombre-corto>`.

---

## Checklist antes de abrir un PR

- [ ] La rama está actualizada respecto a su rama base (`dev` o `main`).
- [ ] La solución compila y los tests relevantes pasan (`dotnet build` / `dotnet test`).
- [ ] El título y descripción del PR explican el *qué* y el *por qué* del cambio.
- [ ] La rama destino del PR es la correcta (`dev` para features, `main` y `dev` para releases/hotfixes).
