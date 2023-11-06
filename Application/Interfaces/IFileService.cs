using Application.Models;

namespace Application.Interfaces;

public interface IFileService
{
    #region Methods

    IEnumerable<Session> Import();

    string Export();

    string Backup();

    #endregion
}