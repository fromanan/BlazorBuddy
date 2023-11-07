using Application.Data;
using Application.Extensions;
using Application.Interfaces;
using Application.Models;

namespace Application.Services;

public class DatabaseService : IDatabaseService, IDisposable, IAsyncDisposable
{
    #region Data Members

    private readonly SessionContext _context;

    #endregion
    
    #region Constructors

    public DatabaseService(SessionContext context)
    {
        _context = context;
    }

    #endregion
    
    #region Public Methods

    public void AddSession(Session session)
    {
        _context.Sessions.Add(session);
        session.Windows.Each(AddWindow);
    }

    public void AddWindow(Window window)
    {
        _context.Windows.Add(window);
        window.Tabs.Each(AddTab);
    }

    public void AddTab(Tab tab)
    {
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