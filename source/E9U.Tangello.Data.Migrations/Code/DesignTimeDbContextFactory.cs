using System;

using Microsoft.Extensions.DependencyInjection;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace E9U.Tangello.Data.Migrations
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MainDbContext>
    {
        public MainDbContext CreateDbContext(string[] args)
        {
            var serviceCollection = new ServiceCollection()
                .AddMainContext();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var mainDbContext = serviceProvider.GetRequiredService<MainDbContext>();
            return mainDbContext;
        }
    }
}
