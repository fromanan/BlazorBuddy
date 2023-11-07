namespace Application.Data;

public static class FileSystem
{
    public static class Path
    {
        public static readonly string LocalAppData = 
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    }
    
    public static void CreateDirectory(string filePath)
    {
        if (Directory.GetParent(filePath)?.FullName is { } directoryName && !Directory.Exists(directoryName))
            Directory.CreateDirectory(directoryName);
    }
}