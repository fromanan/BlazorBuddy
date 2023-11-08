using Application.Data;
using Application.Data.Models;

namespace Application.Interfaces;

public interface IDatabaseService
{
    #region Methods

    Task Initialize(RootContext context);

    void AddSession(Session session);

    void AddWindow(Window window);

    void AddTab(Tab tab);

    Task AddSessionAsync(Session session);

    Task AddWindowAsync(Window window);

    Task AddTabAsync(Tab tab);

    IEnumerable<Session> GetSessions();

    #endregion
}