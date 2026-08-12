# Personal Library Management API

API REST en **ASP.NET Core (.NET 10)** para administrar una colección personal de
libros: crear, consultar, actualizar y eliminar. Valida que **no existan dos
libros con el mismo ISBN**, usa **inyección de dependencias** y maneja los errores
con **excepciones personalizadas** traducidas a códigos HTTP.

> ℹ️ **El código está en la rama `master`.** La rama por defecto (`main`) solo
> contiene documentación. Al clonar, cámbiate a `master` (ver más abajo).

---

## Requisitos

Para clonar y ejecutar el proyecto en tu máquina necesitas:

| Requisito | Versión | Cómo verificar |
|-----------|---------|----------------|
| **.NET SDK** | **10.0** o superior | `dotnet --version` |
| **Git** | cualquiera reciente | `git --version` |

- **Instalar .NET 10 SDK:** https://dotnet.microsoft.com/download/dotnet/10.0
  (en Ubuntu/Debian también sirve `sudo apt install dotnet-sdk-10.0`).
- **No hay dependencias que instalar a mano.** Los paquetes NuGet los descarga
  `dotnet` automáticamente en el primer `run`/`build` a partir de
  `PersonalLibrary.csproj` (ese archivo es el equivalente al `requirements.txt`
  de otros lenguajes).

---

## Cómo clonar y ejecutar

```bash
# 1. Clonar el repositorio
git clone https://github.com/elvis-v7/porgramacion-mobil-Proyecto-5.git
cd porgramacion-mobil-Proyecto-5

# 2. Cambiar a la rama con el código
git checkout master

# 3. Restaurar dependencias y ejecutar
dotnet run
```

Al iniciar verás:

```
Now listening on: http://localhost:5091
```

La API queda escuchando en **http://localhost:5091**. Para detenerla: **Ctrl + C**.

> Los datos se guardan **en memoria** (`InMemoryBookRepository`), así que se
> reinician cada vez que reinicias la aplicación.

---

## Endpoints

| Método | Ruta | Descripción | Respuesta OK |
|--------|------|-------------|--------------|
| GET | `/books` | Lista todos los libros | 200 |
| GET | `/books/{id}` | Obtiene un libro por su ID | 200 |
| POST | `/books` | Crea un libro | 201 |
| PUT | `/books/{id}` | Actualiza un libro | 200 |
| DELETE | `/books/{id}` | Elimina un libro | 204 |

### Modelo `Book`

```json
{
  "id": 1,
  "title": "El Quijote",
  "author": "Cervantes",
  "isbn": "111",
  "publicationYear": 1605
}
```

---

## Probar la API

Con la aplicación corriendo, en otra terminal:

```bash
# Crear un libro  -> 201 Created
curl -X POST http://localhost:5091/books -H "Content-Type: application/json" \
  -d '{"title":"El Quijote","author":"Cervantes","isbn":"111","publicationYear":1605}'

# Repetir el mismo ISBN  -> 409 Conflict
curl -X POST http://localhost:5091/books -H "Content-Type: application/json" \
  -d '{"title":"Copia","author":"X","isbn":"111","publicationYear":2000}'

# Listar todos  -> 200 OK
curl http://localhost:5091/books

# ID inexistente  -> 404 Not Found
curl http://localhost:5091/books/999
```

Los errores se responden con un JSON uniforme:

```json
{ "status": 409, "error": "Conflict", "message": "El ISBN '111' ya existe." }
```

---

## Reglas de negocio

- El ISBN de cada libro debe ser **único**.
- Registrar o actualizar con un ISBN duplicado lanza `DuplicateIsbnException`
  y el servidor responde **409 Conflict**.
- Pedir/actualizar/borrar un ID que no existe responde **404 Not Found**.
- La lógica de negocio vive en la capa de **servicios** (inyección de
  dependencias); los controladores no la contienen.

---

## Estructura del proyecto

```
Controllers/    → endpoints HTTP (respuestas 200/201/204)
Services/       → reglas de negocio y validaciones (ISBN único)
Exceptions/     → excepción personalizada DuplicateIsbnException
Middleware/     → manejo central de errores → HTTP 409 / 404 / 500
Repositories/   → persistencia en memoria
Models/         → entidad Book
Program.cs      → arranque, inyección de dependencias y middleware
```
