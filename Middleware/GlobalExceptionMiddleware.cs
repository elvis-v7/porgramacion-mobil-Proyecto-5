using System.Text.Json;

// ============================================================================
//  GlobalExceptionMiddleware  ·  Manejo central de errores.
//  Envuelve toda peticion en un try/catch. Si cualquier capa lanza una
//  excepcion, la atrapa aqui y la traduce al codigo HTTP correcto.
// ============================================================================
public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // Deja pasar la peticion al resto de la app (controlador y servicio).
            await _next(context);
        }
        // Caso 1: ISBN duplicado -> 409 Conflict.
        catch (DuplicateIsbnException ex)
        {
            context.Response.StatusCode = 409;
            context.Response.ContentType = "application/json";

            // Respuesta siempre con el mismo formato JSON: status, error y message.
            await context.Response.WriteAsync(
                JsonSerializer.Serialize(new
                {
                    status = 409,
                    error = "Conflict",
                    message = ex.Message
                }));
        }
        // Caso 2: recurso no encontrado -> 404 Not Found.
        catch (KeyNotFoundException ex)
        {
            context.Response.StatusCode = 404;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(new
                {
                    status = 404,
                    error = "Not Found",
                    message = ex.Message
                }));
        }
        // Caso 3: cualquier otro error inesperado -> 500 (sin filtrar detalles internos).
        catch (Exception)
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(new
                {
                    status = 500,
                    error = "Internal Server Error"
                }));
        }
    }
}
