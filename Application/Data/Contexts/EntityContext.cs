using Microsoft.EntityFrameworkCore;

namespace Application.Data;

public class EntityContext : DbContext
{
    #region Constructor
    
    public EntityContext(DbContextOptions options) : base(options) { }

    #endregion
    
    #region Public Methods

    public async Task Initialize()
    {
        await Database.MigrateAsync();
    }
    
    #endregion
}