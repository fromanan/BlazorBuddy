using Application.Data;
using Application.Models;
using Microsoft.AspNetCore.Components;

namespace Application.Interfaces;

public interface ISessionService
{
    #region Events

    event Action<int> SelectionChanged;
    
    event Action SessionsUpdated;

    #endregion

    #region Properties

    public int SelectedSessionId { get; }

    #endregion

    #region Methods

    void Initialize(IContextMenuService contextMenuService);

    void UpdateSelection(int id);

    int AddSession(Session session);

    RenderFragment RenderSessionGroup(SessionType type);

    RenderFragment RenderSelectedSession();

    void SaveCurrentSession(string? name = null);
    
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

    void SaveSession(int sessionId);

    void ImportSession(string fileContents);

    #endregion
}