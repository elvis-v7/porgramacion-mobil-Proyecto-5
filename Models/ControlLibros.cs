//modelo de datos para saber como es un libro (partes que tiene)
public class Book {
    public int Id { get; set; } //identificador
    public string Title { get; set; } // nombre del libro
    public string Author { get; set; } //autor
    public string ISBN { get; set; } //codigo unico para cada libro
    public int PublicationYear { get; set; } //año de publicacion
}
