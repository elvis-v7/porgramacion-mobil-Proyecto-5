// Auditoria: modelo de datos que representa la estructura de un libro dentro del sistema.
public class Book {
    public int Id { get; set; } // Auditoria: identificador interno del libro.
    public string Title { get; set; } // Auditoria: nombre o titulo del libro.
    public string Author { get; set; } // Auditoria: autor principal del libro.
    public string ISBN { get; set; } // Auditoria: codigo unico usado para distinguir libros.
    public int PublicationYear { get; set; } // Auditoria: anio de publicacion del libro.
}
