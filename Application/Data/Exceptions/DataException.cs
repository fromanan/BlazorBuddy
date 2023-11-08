namespace Application.Data.Exceptions;

public class DataException : RecoverableException
{
    public DataException() { }
        
    public DataException(string message) : base(message) { }
        
    public DataException(string message, Exception innerException) : base(message, innerException) { }
}