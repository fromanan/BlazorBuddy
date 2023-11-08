namespace Application.Data.Exceptions;

public class RecoverableException : Exception
{
    public RecoverableException() { }
        
    public RecoverableException(string message) : base(message) { }
        
    public RecoverableException(string message, Exception innerException) : base(message, innerException) { }
}