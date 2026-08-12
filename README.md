# Personal Library Management API

> ⚠️ **El código está en la rama `master`, no en `main`.**
> Esta rama (`main`) solo contiene documentación. Para clonar y ejecutar:
>
> ```bash
> git clone https://github.com/elvis-v7/porgramacion-mobil-Proyecto-5.git
> cd porgramacion-mobil-Proyecto-5
> git checkout master
> dotnet run
> ```
>
> Requisitos e instrucciones completas (endpoints, pruebas, requisitos de .NET 10)
> en el **README de la rama [`master`](../../tree/master)**.

## Descripción del proyecto

El proyecto consiste en desarrollar una API REST para administrar una colección personal de libros físicos.

La aplicación permitirá registrar, consultar, actualizar y eliminar libros. También deberá validar que no existan dos libros con el mismo ISBN, utilizar inyección de dependencias para la capa de servicios y manejar los errores mediante excepciones personalizadas.

## Modelo de datos

Cada libro tendrá los siguientes campos:

- `id`: identificador único del libro.
- `title`: título del libro.
- `author`: nombre del autor.
- `isbn`: identificador ISBN único.
- `publicationYear`: año de publicación.

## Endpoints requeridos

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| GET | `/books` | Obtener todos los libros |
| GET | `/books/{id}` | Obtener un libro por su ID |
| POST | `/books` | Registrar un nuevo libro |
| PUT | `/books/{id}` | Actualizar un libro existente |
| DELETE | `/books/{id}` | Eliminar un libro |

## Reglas principales

- El ISBN de cada libro debe ser único.
- Al intentar registrar o actualizar un libro con un ISBN duplicado, se lanzará una excepción personalizada.
- El controlador deberá responder con el código HTTP `409 Conflict` cuando se detecte un ISBN duplicado.
- La capa de servicios deberá configurarse mediante inyección de dependencias.
- Los controladores no deberán contener directamente la lógica de negocio.

# Distribución del trabajo

## Hugo — Estructura del proyecto y persistencia

Hugo será responsable de preparar la estructura base del proyecto y la capa encargada del almacenamiento de los libros.

### Actividades

- Crear y configurar el proyecto.
- Organizar la estructura de carpetas y paquetes.
- Crear la entidad o modelo `Book`.
- Definir los campos:
  - `id`
  - `title`
  - `author`
  - `isbn`
  - `publicationYear`
- Crear el repositorio o mecanismo de persistencia.
- Configurar la restricción de ISBN único en la base de datos, si se utiliza una.
- Configurar las dependencias generales del proyecto.
- Preparar la configuración inicial para la inyección de dependencias.
- Crear datos de prueba cuando sean necesarios.
- Apoyar en la integración final de las ramas.

### Rama de trabajo

```bash
feature/project-structure
