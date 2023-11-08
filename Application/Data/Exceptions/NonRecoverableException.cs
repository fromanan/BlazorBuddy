namespace Application.Data.Exceptions;

public class NonRecoverableException : Exception
{
    public NonRecoverableException() { }
        
    public NonRecoverableException(string message) : base(message) { }
        
    public NonRecoverableException(string message, Exception innerException) : base(message, innerException) { }
}