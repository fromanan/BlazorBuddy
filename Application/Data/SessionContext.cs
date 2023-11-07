using Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Application.Data;

public class SessionContext : DbContext
{
    #region Properties

    public DbSet<Session> Sessions { get; set; }

    public DbSet<Window> Windows { get; set; }

    public DbSet<Tab> Tabs { get; set; }

    #endregion

    #region Constructor
    
    public SessionContext(DbContextOptions<SessionContext> options)
        : base(options)
    {
        Sessions = base.Set<Session>();
        Windows = base.Set<Window>();
        Tabs = base.Set<Tab>();
    }

    #endregion

    #region Public Methods

    public async Task Initialize()
    {
        await Database.EnsureCreatedAsync();
    }

    #endregion

    #region Overrides

    public override EntityEntry<TEntity> Add<TEntity>(TEntity entity)
    {
        switch (entity)
        {
            case Session session:
                Sessions.Add(session);
                break;
            case Window window:
                Windows.Add(window);
                break;
            case Tab tab:
                Tabs.Add(tab);
                break;
        }
        
        return base.Add(entity);
    }

    #endregion
}