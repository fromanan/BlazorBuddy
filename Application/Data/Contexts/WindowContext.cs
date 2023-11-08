using Application.Data.Exceptions;
using Application.Data.Models;
using Application.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Application.Data;

public class WindowContext : EntityContext
{
    #region Data Members

    public DbSet<Window> Windows { get; set; }

    #endregion
    
    #region Constructor
    
    public WindowContext(DbContextOptions<WindowContext> options) : base(options)
    {
        Windows = base.Set<Window>();
    }

    #endregion
    
    #region Overrides

    public override EntityEntry<TEntity> Add<TEntity>(TEntity entity)
    {
        if (entity is not Window window)
            return base.Add(entity);
        if (Windows.ContainsId(window))
            throw new DataException("Attempted to add duplicate entry!");
        return (Windows.Add(window) as EntityEntry<TEntity>)!;
    }

    #endregion
}