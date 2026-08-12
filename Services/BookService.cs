// ============================================================================
//  BookService  ·  Reglas de negocio (validaciones).
//  Aqui vive la regla principal: el ISBN debe ser unico. El controlador no
//  valida; valida esta capa.
// ============================================================================
public class BookService : IBookService
{
    // Repositorio inyectado (inyeccion de dependencias): el servicio no depende
    // de una base de datos concreta.
    private readonly IBookRepository _repository;

    public BookService(IBookRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<Book> GetAll()
    {
        return _repository.GetAll();
    }

    public Book GetById(int id)
    {
        var book = _repository.GetById(id);

        // Si el libro no existe se lanza una excepcion (el middleware la vuelve 404).
        if (book == null)
            throw new KeyNotFoundException(
                $"No se encontró el libro con ID {id}.");

        return book;
    }

    public Book Create(Book book)
    {
        // Normaliza el ISBN: " 111 " y "111" no deben contar como distintos.
        book.ISBN = book.ISBN.Trim();

        // Validacion clave: si el ISBN ya existe, lanza la excepcion propia y no
        // guarda nada (termina en un 409).
        if (_repository.ExistsByIsbn(book.ISBN))
            throw new DuplicateIsbnException(book.ISBN);

        // Solo si pasa la validacion se guarda.
        return _repository.Add(book);
    }

    public Book Update(int id, Book book)
    {
        var existingBook = _repository.GetById(id);

        if (existingBook == null)
            throw new KeyNotFoundException(
                $"No se encontró el libro con ID {id}.");

        book.ISBN = book.ISBN.Trim();

        // Al actualizar se usa ExistsByIsbnExceptId: revisa si el ISBN esta en OTRO
        // libro distinto a este, para que un libro conserve su propio ISBN.
        if (_repository.ExistsByIsbnExceptId(book.ISBN, id))
            throw new DuplicateIsbnException(book.ISBN);

        existingBook.Title = book.Title;
        existingBook.Author = book.Author;
        existingBook.ISBN = book.ISBN;
        existingBook.PublicationYear = book.PublicationYear;

        _repository.Update(existingBook);

        return existingBook;
    }

    public void Delete(int id)
    {
        var book = _repository.GetById(id);

        // Borrar un ID inexistente lanza excepcion -> 404.
        if (book == null)
            throw new KeyNotFoundException(
                $"No se encontró el libro con ID {id}.");

        _repository.Delete(book);
    }
}
