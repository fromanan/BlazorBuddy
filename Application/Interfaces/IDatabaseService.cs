using Application.Models;

namespace Application.Interfaces;

public interface IDatabaseService
{
    #region Methods

    Task Initialize();

    void AddSession(Session session);

    void AddWindow(Window window);

    void AddTab(Tab tab);

    IEnumerable<Session> GetSessions();

    #endregion
}