using Application.Data;
using Application.Data.Models;
using Microsoft.AspNetCore.Components;

namespace Application.Interfaces;

public interface ISessionService : IUpdateTrigger
{
    #region Events

    event Action<int> SelectionChanged;
    
    event Action SessionsUpdated;

    #endregion

    #region Properties

    public int SelectedSessionId { get; }
    
    public Session SelectedSession { get; }

    #endregion

    #region Methods

    Task Initialize(IContextMenuService contextMenuService, IDatabaseService databaseService);

    void UpdateSelection(int id);

    Task AddSession(Session session);
    
    void CloseSession(int sessionId);

    void CloseWindow(int sessionId, int windowId);

    void SaveAndCloseWindow(int sessionId, int windowId);

    void CloseTab(int sessionId, int windowId, int tabId);

    void RenameWindow(int sessionId, int windowId, string? newName);

    void RenameSession(int sessionId, string? newName);

    void DuplicateSession(int sessionId, string? newName);

    void DeleteSession(int sessionId);

    void UnifyWindows(int sessionId);

    void OverwriteSession(int sessionId, int otherSessionId = -1);

    Task SaveSession(int sessionId, string? name = null);

    Task SaveSession(Session session, string? name = null);
    
    Task SaveCurrentSession(string? name = null);

    Task ImportSession(string fileContents);
    
    RenderFragment RenderSessionGroup(SessionType type);

    RenderFragment RenderSelectedSession();

    #endregion
}