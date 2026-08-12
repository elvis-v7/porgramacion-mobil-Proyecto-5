using Microsoft.AspNetCore.Mvc;

// ============================================================================
//  BooksController  ·  Respuestas HTTP (controlador limpio).
//  Solo recibe la peticion, delega al servicio y devuelve el codigo HTTP
//  correcto. No tiene validaciones ni try/catch: si algo falla, la excepcion
//  salta al middleware.
// ============================================================================
[ApiController]
[Route("books")]
public class BooksController : ControllerBase
{
    // Servicio inyectado por inyeccion de dependencias.
    private readonly IBookService _service;

    public BooksController(IBookService service)
    {
        _service = service;
    }

    // GET /books -> 200 OK con la lista.
    [HttpGet]
    public ActionResult<IEnumerable<Book>> GetAll()
    {
        return Ok(_service.GetAll());
    }

    // GET /books/{id} -> 200 OK. Si no existe, el middleware responde 404.
    [HttpGet("{id}")]
    public ActionResult<Book> GetById(int id)
    {
        return Ok(_service.GetById(id));
    }

    // POST /books -> 201 Created. No valida el ISBN aqui; si esta duplicado,
    // el middleware responde 409.
    [HttpPost]
    public ActionResult<Book> Create(Book book)
    {
        var created = _service.Create(book);

        return CreatedAtAction(
            nameof(GetById),
            new { id = created.Id },
            created);
    }

    // PUT /books/{id} -> 200 OK con el libro actualizado.
    [HttpPut("{id}")]
    public ActionResult<Book> Update(int id, Book book)
    {
        return Ok(_service.Update(id, book));
    }

    // DELETE /books/{id} -> 204 No Content (borrado ok, sin cuerpo).
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _service.Delete(id);
        return NoContent();
    }
}
