public class InMemoryBookRepository : IBookRepository
{
    private readonly List<Book> _books = new();
    private int _nextId = 1;

    public IEnumerable<Book> GetAll()
    {
        return _books;
    }

    public Book? GetById(int id)
    {
        return _books.FirstOrDefault(b => b.Id == id);
    }

    public Book Add(Book book)
    {
        book.Id = _nextId++;
        _books.Add(book);
        return book;
    }

    public void Update(Book book)
    {
        var index = _books.FindIndex(b => b.Id == book.Id);

        if (index != -1)
            _books[index] = book;
    }

    public void Delete(Book book)
    {
        _books.Remove(book);
    }

    public bool ExistsByIsbn(string isbn)
    {
        return _books.Any(b =>
            b.ISBN.Equals(isbn, StringComparison.OrdinalIgnoreCase));
    }

    public bool ExistsByIsbnExceptId(string isbn, int id)
    {
        return _books.Any(b =>
            b.Id != id &&
            b.ISBN.Equals(isbn, StringComparison.OrdinalIgnoreCase));
    }
}