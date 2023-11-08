using Application.Data.Models;
using Application.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Application.Data;

public class RootContext : DbContext
{
    #region Data Members

    private SessionContext SessionContext { get; set; } = null!;
    
    public DbSet<Session> Sessions => SessionContext.Sessions;
    
    private WindowContext WindowContext { get; set; } = null!;
    
    public DbSet<Window> Windows => WindowContext.Windows;
    
    private TabContext TabContext { get; set; } = null!;
    
    public DbSet<Tab> Tabs => TabContext.Tabs;
    
    private const string _COUNT_QUERY = "select {0} from sqlite_sequence where [name] = '{1}'";

    private const string _EXISTS_QUERY = "select exists ( {0} ) as Value";

    #endregion
    
    #region Constructor
    
    public RootContext(DbContextOptions<RootContext> options) : base(options) { }

    #endregion
    
    #region Public Methods

    public async Task Initialize(SessionContext sessionContext, WindowContext windowContext, TabContext tabContext)
    {
        SessionContext = sessionContext;
        WindowContext = windowContext;
        TabContext = tabContext;
        
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

    public override int SaveChanges()
    {
        int result = base.SaveChanges();
        result += SessionContext.SaveChanges();
        result += WindowContext.SaveChanges();
        result += TabContext.SaveChanges();
        return result;
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        int result = base.SaveChanges(acceptAllChangesOnSuccess);
        result += SessionContext.SaveChanges(acceptAllChangesOnSuccess);
        result += WindowContext.SaveChanges(acceptAllChangesOnSuccess);
        result += TabContext.SaveChanges(acceptAllChangesOnSuccess);
        return result;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        int result = await base.SaveChangesAsync(cancellationToken);
        result += await SessionContext.SaveChangesAsync(cancellationToken);
        result += await WindowContext.SaveChangesAsync(cancellationToken);
        result += await TabContext.SaveChangesAsync(cancellationToken);
        return result;
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = new())
    {
        int result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        result += await SessionContext.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        result += await WindowContext.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        result += await TabContext.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        return result;
    }

    #endregion
}