using System;
using Microsoft.EntityFrameworkCore;

namespace DotNetPerformance.Data._base
{
    public class BaseDbInitializer<TContext>
        where TContext : DbContext
    {
        public virtual void Initialize(IServiceProvider serviceProvider, TContext context)
        {
            // Create the Db if it doesn't exist and applies any pending migration
            context.Database.Migrate();
            context.SaveChanges();
        }

        public virtual void RecreateDatabase(TContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.Migrate();
            context.SaveChanges();
        }
    }
}
