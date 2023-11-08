using Application.Data.Exceptions;
using Application.Data.Models;
using Application.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Application.Data;

public class TabContext : EntityContext
{
    #region Data Members

    public DbSet<Tab> Tabs { get; set; }

    #endregion
    
    #region Constructor
    
    public TabContext(DbContextOptions<TabContext> options) : base(options)
    {
        Tabs = base.Set<Tab>();
    }

    #endregion
    
    #region Overrides

    public override EntityEntry<TEntity> Add<TEntity>(TEntity entity)
    {
        if (entity is not Tab tab)
            return base.Add(entity);
        if (Tabs.ContainsId(tab))
            throw new DataException("Attempted to add duplicate entry!");
        return (Tabs.Add(tab) as EntityEntry<TEntity>)!;
    }

    #endregion
}