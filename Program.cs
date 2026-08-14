var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Dependency Injection
builder.Services.AddSingleton<IBookRepository, InMemoryBookRepository>();
builder.Services.AddScoped<IBookService, BookService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Auditoria: fuerza la redireccion a HTTPS para que las solicitudes viajen cifradas.
app.UseHttpsRedirection();

// Sirve el frontend estatico (wwwroot/index.html) en la raiz "/".
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseMiddleware<GlobalExceptionMiddleware>();

app.MapControllers();

// Auditoria: inicia la aplicacion web y deja escuchando los endpoints configurados.
app.Run();
