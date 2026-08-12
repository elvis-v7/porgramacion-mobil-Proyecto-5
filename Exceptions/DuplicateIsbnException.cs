// ============================================================================
//  DuplicateIsbnException  ·  Excepcion personalizada.
//  Hereda de Exception. Al ser un tipo propio, el middleware la distingue del
//  resto de errores y le asigna un codigo HTTP especifico (el 409).
// ============================================================================
public class DuplicateIsbnException : Exception
{
    // Recibe el ISBN y arma el mensaje que vera el usuario.
    public DuplicateIsbnException(string isbn)
        : base($"El ISBN '{isbn}' ya existe.")
    {
    }
}
