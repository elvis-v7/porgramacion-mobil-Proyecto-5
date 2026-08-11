//modelo de datos para saber como es un libro (partes que tiene)
public class Book {
    public int Id { get; set; } //identificador
    public string Title { get; set; } = string.Empty; // nombre del libro
    public string Author { get; set; } = string.Empty; //autor
    public string ISBN { get; set; } = string.Empty; //codigo unico para cada libro
    public int PublicationYear { get; set; } //año de publicacion
}
