using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("books")]
public class BooksController : ControllerBase
{
    private readonly IBookService _service;

    public BooksController(IBookService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Book>> GetAll()
    {
        return Ok(_service.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<Book> GetById(int id)
    {
        return Ok(_service.GetById(id));
    }

    [HttpPost]
    public ActionResult<Book> Create(Book book)
    {
        var created = _service.Create(book);

        return CreatedAtAction(
            nameof(GetById),
            new { id = created.Id },
            created);
    }

    [HttpPut("{id}")]
    public ActionResult<Book> Update(int id, Book book)
    {
        return Ok(_service.Update(id, book));
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _service.Delete(id);
        return NoContent();
    }
}