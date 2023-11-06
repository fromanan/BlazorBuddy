using Application.Interfaces;
using Application.Models;

namespace Application.Services;

public class FileService : IFileService
{
    #region Public Methods

    public IEnumerable<Session> Import()
    {
        return Array.Empty<Session>();
    }

    public string Export()
    {
        return string.Empty;
    }

    public string Backup()
    {
        return string.Empty;
    }

    #endregion
}