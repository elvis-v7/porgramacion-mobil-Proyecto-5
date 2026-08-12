public class BookService : IBookService
{
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

        if (book == null)
            throw new KeyNotFoundException(
                $"No se encontró el libro con ID {id}.");

        return book;
    }

    public Book Create(Book book)
    {
        book.ISBN = book.ISBN.Trim();

        if (_repository.ExistsByIsbn(book.ISBN))
            throw new DuplicateIsbnException(book.ISBN);

        return _repository.Add(book);
    }

    public Book Update(int id, Book book)
    {
        var existingBook = _repository.GetById(id);

        if (existingBook == null)
            throw new KeyNotFoundException(
                $"No se encontró el libro con ID {id}.");

        book.ISBN = book.ISBN.Trim();

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

        if (book == null)
            throw new KeyNotFoundException(
                $"No se encontró el libro con ID {id}.");

        _repository.Delete(book);
    }
}