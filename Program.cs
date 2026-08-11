var builder = WebApplication.CreateBuilder(args);

// Auditoria: registra el servicio de OpenAPI para documentar los endpoints
// disponibles y facilitar las pruebas durante la exposicion.
builder.Services.AddOpenApi();

var app = builder.Build();

// Auditoria: en ambiente de desarrollo se expone la documentacion OpenAPI.
// En produccion no se publica automaticamente para evitar mostrar detalles internos.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Auditoria: fuerza la redireccion a HTTPS para que las solicitudes viajen cifradas.
app.UseHttpsRedirection();

// Auditoria: catalogo de textos usados por el endpoint de ejemplo para describir
// el clima generado aleatoriamente.
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

// Auditoria: endpoint GET de prueba que genera cinco pronosticos del clima.
// No recibe parametros, construye datos aleatorios y responde un arreglo JSON.
app.MapGet("/weatherforecast", () =>
{
    // Auditoria: crea los pronosticos para los proximos cinco dias usando
    // temperatura y resumen aleatorios; no consulta base de datos ni servicios externos.
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

// Auditoria: inicia la aplicacion web y deja escuchando los endpoints configurados.
app.Run();

// Auditoria: modelo de respuesta del endpoint /weatherforecast.
// Agrupa fecha, temperatura en Celsius y una descripcion opcional del clima.
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    // Auditoria: propiedad calculada que convierte Celsius a Fahrenheit al serializar
    // la respuesta, evitando guardar un dato duplicado.
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
