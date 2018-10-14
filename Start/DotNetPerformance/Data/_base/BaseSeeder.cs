using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DotNetPerformance.Data._base
{
    public class BaseSeeder<TContext, TInitiliazer>
        where TContext : DbContext
        where TInitiliazer : BaseDbInitializer<TContext>
    {

        public static void Seed(IApplicationBuilder app)
        {
            // Create a service scope to get an ApplicationDbContext instance using DI
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                try
                {
                    var dbContext = serviceScope.ServiceProvider.GetService<TContext>();
                    // Seed the Db
                    var instance = Activator.CreateInstance<TInitiliazer>();
                    instance.Initialize(serviceScope.ServiceProvider, dbContext);
                }
                catch (Exception ex)
                {
                    var logger = serviceScope.ServiceProvider.GetRequiredService<ILogger<BaseSeeder<TContext, TInitiliazer>>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }
        }
    }
}
