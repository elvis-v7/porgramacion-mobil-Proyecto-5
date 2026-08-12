public interface IBookRepository
{
    IEnumerable<Book> GetAll();
    Book? GetById(int id);
    Book Add(Book book);
    void Update(Book book);
    void Delete(Book book);
    bool ExistsByIsbn(string isbn);
    bool ExistsByIsbnExceptId(string isbn, int id);
}