using Application.Data.Exceptions;
using Application.Extensions;
using Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Application.Data;

public class SessionContext : DbContext
{
    #region Data Members

    private const string _COUNT_QUERY = "select {0} from sqlite_sequence where [name] = '{1}'";

    private const string _EXISTS_QUERY = "select exists ( {0} ) as Value";

    #endregion
    
    #region Properties

    public DbSet<Session> Sessions { get; set; }

    public DbSet<Window> Windows { get; set; }

    public DbSet<Tab> Tabs { get; set; }

    #endregion

    #region Constructor
    
    public SessionContext(DbContextOptions<SessionContext> options) : base(options)
    {
        Sessions = base.Set<Session>();
        Windows  = base.Set<Window>();
        Tabs     = base.Set<Tab>();
    }

    #endregion

    #region Public Methods

    public async Task Initialize()
    {
        await Database.MigrateAsync();
    }

    private async Task<bool> Exists(string innerQuery)
    {
        return await Database.SqlQueryRaw<bool>(_EXISTS_QUERY.Format(innerQuery)).SingleAsync();
    }

    public async Task<int> GetCurrentId(Type entityType)
    {
        if (!await Exists(_COUNT_QUERY.Format(1, $"{entityType.Name}s")))
            return 0;
        
        return await Database.SqlQueryRaw<int>(_COUNT_QUERY.Format("seq as Value", $"{entityType.Name}s")).SingleAsync();
    }

    #endregion

    #region Overrides

    public override EntityEntry<TEntity> Add<TEntity>(TEntity entity)
    {
        switch (entity)
        {
            case Session session:
                if (Sessions.ContainsId(session))
                    throw new DataException("Attempted to add duplicate entry!");
                Sessions.Add(session);
                break;
            case Window window:
                if (Windows.ContainsId(window))
                    throw new DataException("Attempted to add duplicate entry!");
                Windows.Add(window);
                break;
            case Tab tab:
                if (Tabs.ContainsId(tab))
                    throw new DataException("Attempted to add duplicate entry!");
                Tabs.Add(tab);
                break;
        }
        
        return base.Add(entity);
    }

    #endregion
}