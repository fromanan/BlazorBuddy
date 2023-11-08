using Microsoft.EntityFrameworkCore;

namespace Application.Data;

public class EntityContext : DbContext
{
    #region Constructor
    
    public EntityContext(DbContextOptions options) : base(options) { }

    #endregion
}