using System.ComponentModel;
using Application.Data;
using Application.Elements;
using Application.Elements.ContextMenus;
using Application.Extensions;
using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Application.Services;

public class SessionService : ISessionService
{
    #region Data Members

    private readonly Dictionary<int, Session> _sessionTable = new();

    private WindowMenu? _windowMenu;
    
    private int _insertRow;

    #endregion
    
    #region Properties

    public int SelectedSessionId { get; private set; }

    private Session SelectedSession => _sessionTable[SelectedSessionId];

    #endregion

    #region Events

    public event Action<int> SelectionChanged = delegate {  };
    
    public event Action SessionsUpdated = delegate {  };

    #endregion

    #region Sessions

    // TODO: This will be replaced with dynamic loading from DB later
    
    public readonly Session CurrentSession = new()
    {
        LastChange = DateTime.Now.AddSeconds(-3),
        SessionType = SessionType.Current,
        Windows =
        {
            new Window
            {
                Tabs =
                {
                    new Tab
                    {
                        Title = "blazor custom component not working - Google Search",
                        Url = "https://www.google.com/search?q=blazor+custom+component+not+working&ie=UTF-8"
                    }
                }
            }
        }
    };
    
    public readonly List<Session> PreviousSessions = new()
    {
        new Session
        {
            LastChange = DateTime.Now.AddMinutes(-44),
            SessionType = SessionType.Previous,
            Windows =
            {
                new Window
                {
                    Tabs =
                    {
                        new Tab
                        {
                            Title = "blazor custom component not working - Google Search",
                            Url = "https://www.google.com/search?q=blazor+custom+component+not+working&ie=UTF-8"
                        }
                    }
                }
            }
        },
        new Session
        {
            LastChange = DateTime.Now.AddMinutes(-124),
            SessionType = SessionType.Previous
        }
    };
    
    public readonly List<Session> SavedSessions = new()
    {
        new Session
        {
            LastChange = DateTime.Now.AddHours(-2),
            SessionType = SessionType.Saved,
            Windows =
            {
                new Window
                {
                    Tabs =
                    {
                        new Tab
                        {
                            Title = "blazor custom component not working - Google Search",
                            Url = "https://www.google.com/search?q=blazor+custom+component+not+working&ie=UTF-8"
                        }
                    }
                }
            }
        }
    };

    #endregion

    #region Constructor

    public SessionService()
    {
        // Current Session will always have the id = 0
        AddSession(CurrentSession);

        foreach (Session session in PreviousSessions)
        {
            AddSession(session);
        }

        foreach (Session session in SavedSessions)
        {
            AddSession(session);
        }
    }

    #endregion
    
    #region Public Methods

    public void Initialize(IContextMenuService contextMenuService)
    {
        _windowMenu = contextMenuService.GetMenu<WindowMenu>(ContextMenuId.WINDOW);
    }
    
    public void UpdateSelection(int id)
    {
        if (id == SelectedSessionId)
            return;
        SelectedSessionId = id;
        SelectionChanged(id);
        if (_windowMenu is { } windowMenu)
        {
            windowMenu.IsCurrent = SelectedSession.SessionType is SessionType.Current;
        }
    }

    public int AddSession(Session session)
    {
        session.Identifier = _insertRow;
        _sessionTable.Add(session.Identifier, session);
        return _insertRow++;
    }
    
    public RenderFragment RenderSessionGroup(SessionType type)
    {
        return _RenderFragment;

        void _RenderFragment(RenderTreeBuilder builder)
        {
            builder.OpenComponent<SessionGroup>(sequence: 0);
            builder.AddAttribute(sequence: 1, nameof(SessionGroup.Title),        value: GetSessionTitle(type));
            builder.AddAttribute(sequence: 2, nameof(SessionGroup.SessionType),  value: type);
            builder.AddAttribute(sequence: 3, nameof(SessionGroup.ChildContent), value: (RenderFragment)_RenderChildContent);
            builder.CloseComponent();
        }

        void _RenderChildContent(RenderTreeBuilder builder)
        {
            GetSessionsByType(type).Each(_RenderSessionRow);
            return;
                
            void _RenderSessionRow(Session session)
            {
                builder.OpenComponent<SessionRow>(sequence: 4);
                builder.AddAttribute(sequence: 5, nameof(SessionRow.Title),       value: session.Title);
                builder.AddAttribute(sequence: 6, nameof(SessionRow.LastChange),  value: session.LastChange);
                builder.AddAttribute(sequence: 7, nameof(SessionRow.TabCount),    value: session.TabCount);
                builder.AddAttribute(sequence: 8, nameof(SessionRow.SessionType), value: session.SessionType);
                builder.AddAttribute(sequence: 9,  nameof(SessionRow.Selected),    value: SelectedSessionId == session.Identifier);
                builder.AddAttribute(sequence: 10, nameof(SessionRow.Identifier),  value: session.Identifier);
                builder.CloseComponent();
            }
        }
    }

    public RenderFragment RenderSelectedSession()
    {
        Session session = SelectedSession;
        
        return _RenderFragment;

        void _RenderFragment(RenderTreeBuilder builder)
        {
            builder.OpenComponent<SessionLayout>(sequence: 0);
            builder.AddAttribute(sequence: 1, nameof(SessionLayout.Title),       value: session.Title);
            builder.AddAttribute(sequence: 2, nameof(SessionLayout.WindowCount), value: session.Windows.Count);
            builder.AddAttribute(sequence: 3, nameof(SessionLayout.TabCount),    value: session.TabCount);
            builder.AddAttribute(sequence: 4, nameof(SessionLayout.LastChange),  value: session.LastChange);
            builder.AddAttribute(sequence: 5, nameof(SessionLayout.Windows),     value: session.Windows);
            builder.AddAttribute(sequence: 6, nameof(SessionLayout.SessionType), value: session.SessionType);
            builder.CloseComponent();
        }
    }
    
    public void SaveCurrentSession(string? name = null)
    {
        Session session = CurrentSession.Copy();
        session.Title = name ?? "Unnamed session";
        session.Created = DateTime.Now;
        AddSession(session);
        SavedSessions.Add(session);
        SessionsUpdated();
    }

    public void CloseSession(int sessionId)
    {
        Console.WriteLine("Closing session!");
    }

    public void CloseWindow(int sessionId, int windowId)
    {
        Console.WriteLine("Closing window!");
    }

    public void SaveAndCloseWindow(int sessionId, int windowId)
    {
        Console.WriteLine("Saving & Closing window!");
    }

    public void CloseTab(int sessionId, int windowId, int tabId)
    {
        Console.WriteLine("Closing tab!");
    }

    public void RenameWindow(int sessionId, int windowId, string? newName)
    {
        Console.WriteLine("Renaming window!");
    }

    public void RenameSession(int sessionId, string? newName)
    {
        Console.WriteLine("Renaming session!");
    }

    public void DuplicateSession(int sessionId, string? newName)
    {
        Console.WriteLine("Duplicating session!");
    }

    public void DeleteSession(int sessionId)
    {
        Console.WriteLine("Deleting session!");
    }

    public void UnifyWindows(int sessionId)
    {
        Console.WriteLine("Unifying windows!");
    }

    public void OverwriteSession(int sessionId, int otherSessionId = -1)
    {
        Console.WriteLine("Overwriting session!");
    }

    public void SaveSession(int sessionId)
    {
        Console.WriteLine("Saving session!");
    }

    public void ImportSession(string fileContents)
    {
        Console.WriteLine("Importing session!");
    }

    #endregion

    #region Private Methods

    private string GetSessionTitle(SessionType type)
    {
        return type switch
        {
            SessionType.Current  => "Current Session",
            SessionType.Previous => "Previous Sessions",
            SessionType.Saved    => "Saved Sessions",
            SessionType.Updated  => "Saved Sessions",
            _                    => throw new InvalidEnumArgumentException($"Invalid session type '{type}'")
        };
    }

    private IEnumerable<Session> GetSessionsByType(SessionType type)
    {
        return type switch
        {
            SessionType.Current  => new [] { CurrentSession },
            SessionType.Previous => PreviousSessions,
            SessionType.Saved    => SavedSessions,
            SessionType.Updated  => SavedSessions,
            _                    => throw new InvalidEnumArgumentException($"Invalid session type '{type}'")
        };
    }

    #endregion
}