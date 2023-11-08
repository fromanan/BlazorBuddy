using Application.Data.Exceptions;
using Application.Data.Models;
using Application.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Application.Data;

public class SessionContext : EntityContext
{
    #region Data Members

    public DbSet<Session> Sessions { get; set; }

    #endregion

    #region Constructor
    
    public SessionContext(DbContextOptions<SessionContext> options) : base(options)
    {
        Sessions = base.Set<Session>();
    }

    #endregion

    #region Overrides

    public override EntityEntry<TEntity> Add<TEntity>(TEntity entity)
    {
        if (entity is not Session session)
            return base.Add(entity);
        if (Sessions.ContainsId(session))
            throw new DataException("Attempted to add duplicate entry!");
        return (Sessions.Add(session) as EntityEntry<TEntity>)!;
    }

    #endregion
}