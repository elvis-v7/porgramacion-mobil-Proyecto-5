// Auditoria: modelo de datos que representa la estructura de un libro dentro del sistema.
public class Book {
    public int Id { get; set; } // identificador interno del libro
    public string Title { get; set; } = string.Empty; // nombre o titulo del libro
    public string Author { get; set; } = string.Empty; // autor principal del libro
    public string ISBN { get; set; } = string.Empty; // codigo unico que distingue cada libro
    public int PublicationYear { get; set; } // anio de publicacion
}
