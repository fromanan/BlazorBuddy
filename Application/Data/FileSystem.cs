namespace Application.Data;

public static class FileSystem
{
    #region Public Methods

    public static class Path
    {
        public static readonly string LocalAppData = 
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    }

    private static string? GetParentDirectory(string filepath)
    {
        return Directory.GetParent(filepath)?.FullName;
    }
    
    public static void CreateDirectory(string filepath)
    {
        try
        {
            if (GetParentDirectory(filepath) is not { } directoryName)
                throw new FileSystemException($"Failed to retrieve parent directory for filepath: '{filepath}'");

            if (Directory.Exists(directoryName))
                return;
            
            Directory.CreateDirectory(directoryName);
        }
        catch (Exception exception)
        {
            throw new FileSystemException("Failed to generate required directories", exception);
        }
    }

    #endregion
    
    #region Embedded Types

    public class FileSystemException : Exception
    {
        public FileSystemException() { }
        
        public FileSystemException(string message) : base(message) { }
        
        public FileSystemException(string message, Exception innerException) : base(message, innerException) { }
    }

    #endregion
}