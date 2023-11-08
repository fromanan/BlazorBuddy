using Application.Data;
using Application.Extensions;
using Application.Interfaces;
using Application.Models;

namespace Application.Services;

public class DatabaseService : IDatabaseService, IDisposable, IAsyncDisposable
{
    #region Data Members

    private readonly SessionContext _context;
    
    private int _insertRowSessions;
    
    private int _insertRowWindows;
    
    private int _insertRowTabs;

    #endregion
    
    #region Constructors

    public DatabaseService(SessionContext context)
    {
        _context = context;
    }

    #endregion
    
    #region Public Methods
    
    public async Task Initialize()
    {
        _insertRowSessions = await _context.GetCurrentId(typeof(Session)) + 1;
        _insertRowWindows  = await _context.GetCurrentId(typeof(Window)) + 1;
        _insertRowTabs     = await _context.GetCurrentId(typeof(Tab)) + 1;

        // Reserve 0 for CurrentSession
        if (_insertRowSessions is 0)
            _insertRowSessions++;
    }

    public void AddSession(Session session)
    {
        session.Id = _insertRowSessions++;
        
        _context.Sessions.Add(session);
        
        foreach (Window window in session.Windows)
        {
            window.SessionId = session.Id;
            AddWindow(window);
        }
    }

    public void AddWindow(Window window)
    {
        window.Id = _insertRowWindows++;
        
        _context.Windows.Add(window);
        
        foreach (Tab tab in window.Tabs)
        {
            tab.WindowId = window.Id;
            AddTab(tab);
        }
    }

    public void AddTab(Tab tab)
    {
        tab.Id = _insertRowTabs++;
        _context.Tabs.Add(tab);
    }

    public async Task AddSessionAsync(Session session)
    {
        session.Id = _insertRowSessions++;
        await _context.Sessions.AddAsync(session);
        await session.Windows.EachAsync(AddWindowAsync);
    }

    public async Task AddWindowAsync(Window window)
    {
        window.Id = _insertRowWindows++;
        await _context.Windows.AddAsync(window);
        await window.Tabs.EachAsync(AddTabAsync);
    }

    public async Task AddTabAsync(Tab tab)
    {
        tab.Id = _insertRowTabs++;
        await _context.Tabs.AddAsync(tab);
    }

    public IEnumerable<Session> GetSessions()
    {
        return _context.Sessions.Where(s => s.SessionType == SessionType.Saved);
    }

    public static string InitializeEnvironment(IConfiguration configuration)
    {
        if (configuration.GetValue<string>("DatabasePath") is not { } databasePathTemplate)
            throw new DatabaseException("Failed to load database path");

        string databasePath = string.Format(databasePathTemplate, FileSystem.Path.LocalAppData);
        try
        {
            FileSystem.CreateDirectory(databasePath);
        }
        catch (Exception exception)
        {
            throw new DatabaseException("Failed to initialize database environment", exception);
        }

        return databasePath;
    }

    #endregion

    #region IDisposable Implementation

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        _context.SaveChanges();
        _context.Dispose();
    }

    #endregion

    #region IAsyncDisposable Implementation

    public async ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        await _context.SaveChangesAsync();
        await _context.DisposeAsync();
    }

    #endregion

    #region Embedded Types

    public class DatabaseException : Exception
    {
        public DatabaseException() { }
        
        public DatabaseException(string message) : base(message) { }
        
        public DatabaseException(string message, Exception innerException) : base(message, innerException) { }
    }

    #endregion
}