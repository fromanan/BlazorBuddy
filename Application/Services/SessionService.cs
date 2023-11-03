using System.ComponentModel;
using System.Runtime.CompilerServices;
using Application.Data;
using Application.Elements;
using Application.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Application.Services;

public static class SessionService
{
    private static readonly Dictionary<int, Session> _SessionTable = new();

    public static int SelectedSessionId { get; private set; }

    private static Session SelectedSession => _SessionTable[SelectedSessionId];

    private static int _InsertRow;
    
    public static event Action<int> SelectionChanged = delegate {  };
    
    public static event Action SessionsUpdated = delegate {  };
    
    // TODO: This will be replaced with dynamic loading from DB later
    
    public static readonly Session CurrentSession = new()
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
    
    public static readonly List<Session> PreviousSessions = new()
    {
        new Session
        {
            LastChange = DateTime.Now.AddMinutes(-44),
            SessionType = SessionType.Previous
        },
        new Session
        {
            LastChange = DateTime.Now.AddMinutes(-124),
            SessionType = SessionType.Previous
        }
    };
    
    public static readonly List<Session> SavedSessions = new()
    {
        new Session
        {
            LastChange = DateTime.Now.AddHours(-2),
            SessionType = SessionType.Saved
        }
    };
    
    [ModuleInitializer]
    public static void Init()
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
    
    public static void UpdateSelection(int id)
    {
        if (id == SelectedSessionId)
            return;
        SelectedSessionId = id;
        SelectionChanged(id);
    }

    public static int AddSession(Session session)
    {
        session.Identifier = _InsertRow;
        _SessionTable.Add(session.Identifier, session);
        return _InsertRow++;
    }
    
    public static RenderFragment RenderSessionGroup(SessionType type)
    {
        return _RenderFragment;

        void _RenderFragment(RenderTreeBuilder builder)
        {
            builder.OpenComponent<SessionGroup>(0);
            builder.AddAttribute(1, nameof(SessionGroup.Title), GetSessionTitle(type));
            builder.AddAttribute(2, nameof(SessionGroup.SessionType),  type);
            builder.AddAttribute(3, nameof(SessionGroup.ChildContent), (RenderFragment)(innerBuilder => 
            {
                foreach (Session session in GetSessionsByType(type))
                {
                    innerBuilder.OpenComponent<SessionRow>(4);
                    if (session.Title is not null)
                        innerBuilder.AddAttribute(5, nameof(SessionRow.Title), session.Title);
                    innerBuilder.AddAttribute(6, nameof(SessionRow.LastChange), session.LastChange);
                    innerBuilder.AddAttribute(7, nameof(SessionRow.TabCount),   session.TabCount);
                    innerBuilder.AddAttribute(8, nameof(SessionRow.SessionType),       type);
                    innerBuilder.AddAttribute(9, nameof(SessionRow.Selected), SelectedSessionId == session.Identifier);
                    innerBuilder.AddAttribute(10, nameof(SessionRow.Identifier), session.Identifier);
                    innerBuilder.CloseComponent();
                }
            }));
            
            builder.CloseComponent();
        }
    }

    public static RenderFragment RenderSelectedSession()
    {
        Session session = SelectedSession;
        
        return _RenderFragment;

        void _RenderFragment(RenderTreeBuilder builder)
        {
            builder.OpenComponent<SessionLayout>(0);
            if (session.Title is not null)
                builder.AddAttribute(1, nameof(SessionLayout.Title), session.Title);
            builder.AddAttribute(2, nameof(SessionLayout.WindowCount),  session.Windows.Count);
            builder.AddAttribute(3, nameof(SessionLayout.TabCount),     session.TabCount);
            builder.AddAttribute(4, nameof(SessionLayout.LastChange),   session.LastChange);
            builder.AddAttribute(5, nameof(SessionLayout.Windows),      session.Windows);
            builder.AddAttribute(6, nameof(SessionLayout.SessionType),  session.SessionType);
            builder.CloseComponent();
        }
    }

    private static string GetSessionTitle(SessionType type)
    {
        return type switch
        {
            SessionType.Current  => "Current Session",
            SessionType.Previous => "Previous Sessions",
            SessionType.Saved    => "Saved Sessions",
            _                    => throw new InvalidEnumArgumentException($"Invalid session type '{type}'")
        };
    }

    private static IEnumerable<Session> GetSessionsByType(SessionType type)
    {
        return type switch
        {
            SessionType.Current  => new [] { CurrentSession },
            SessionType.Previous => PreviousSessions,
            SessionType.Saved    => SavedSessions,
            _                    => throw new InvalidEnumArgumentException($"Invalid session type '{type}'")
        };
    }

    public static void SaveCurrentSession(string? name = null)
    {
        Session session = CurrentSession.Copy();
        session.Title = name ?? "Unnamed session";
        session.Created = DateTime.Now;
        AddSession(session);
        SavedSessions.Add(session);
        SessionsUpdated();
    }

    public static void CloseSession(int sessionId)
    {
        Console.WriteLine("Closing session!");
    }

    public static void CloseWindow(int sessionId, int windowId)
    {
        Console.WriteLine("Closing window!");
    }

    public static void SaveAndCloseWindow(int sessionId, int windowId)
    {
        Console.WriteLine("Saving & Closing window!");
    }

    public static void CloseTab(int sessionId, int windowId, int tabId)
    {
        Console.WriteLine("Closing tab!");
    }

    public static void RenameWindow(int sessionId, int windowId, string? newName)
    {
        Console.WriteLine("Renaming window!");
    }

    public static void RenameSession(int sessionId, string? newName)
    {
        Console.WriteLine("Renaming session!");
    }

    public static void DuplicateSession(int sessionId, string? newName)
    {
        Console.WriteLine("Duplicating session!");
    }

    public static void DeleteSession(int sessionId)
    {
        Console.WriteLine("Deleting session!");
    }

    public static void UnifyWindows(int sessionId)
    {
        Console.WriteLine("Unifying windows!");
    }

    public static void OverwriteSession(int sessionId, int otherSessionId = -1)
    {
        Console.WriteLine("Overwriting session!");
    }

    public static void SaveSession(int sessionId)
    {
        Console.WriteLine("Saving session!");
    }
}