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
        _insertRowSessions = await _context.GetCurrentId(typeof(Session));
        _insertRowWindows = await _context.GetCurrentId(typeof(Window));
        _insertRowTabs = await _context.GetCurrentId(typeof(Tab));

        // Reserve 0 for CurrentSession
        if (_insertRowSessions is 0)
            _insertRowSessions++;
    }

    public void AddSession(Session session)
    {
        session.Id = _insertRowSessions++;
        _context.Sessions.Add(session);
        session.Windows.Each(AddWindow);
    }

    public void AddWindow(Window window)
    {
        window.Id = _insertRowWindows++;
        _context.Windows.Add(window);
        window.Tabs.Each(AddTab);
    }

    public void AddTab(Tab tab)
    {
        tab.Id = _insertRowTabs++;
        _context.Tabs.Add(tab);
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