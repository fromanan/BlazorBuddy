using System.Data;
using Application.Models;
using Microsoft.Data.Sqlite;
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

    public async Task<int> GetCurrentId(Type entityType)
    {
        /*SqliteParameter[] @params =
        {
            new("@returnVal", SqliteType.Integer)
            {
                Direction = ParameterDirection.Output
            },
            new("@tableName", SqliteType.Text)
            {
                Direction = ParameterDirection.Input
            }
        };

        SqliteCommand command = new("@returnVal = select seq from sqlite_sequence where name = @tableName");
        
        command.ExecuteScalarAsync(@params)
        
        await Database.ExecuteSqlAsync(command, @params);

        return Convert.ToInt32(@params[0].Value);*/

        return -1;
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