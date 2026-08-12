public class DuplicateIsbnException : Exception
{
    public DuplicateIsbnException(string isbn)
        : base($"El ISBN '{isbn}' ya existe.")
    {
    }
}