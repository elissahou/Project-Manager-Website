using System;

using Microsoft.Extensions.DependencyInjection;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace E9U.Tangello.Data.Migrations
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MainContext>
    {
        public MainContext CreateDbContext(string[] args)
        {
            var serviceCollection = new ServiceCollection()
                .AddDbContext<MainContext>(dbContextOptionsBuilder =>
                {
                    var connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=E9U.Tangello;Trusted_Connection=True;";
                    dbContextOptionsBuilder.UseSqlServer(connectionString, sqlServerDbContextOptionsBuilder =>
                    {
                        sqlServerDbContextOptionsBuilder.MigrationsAssembly("E9U.Tangello.Data.Migrations");
                    });
                })
                ;

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var dbContext = serviceProvider.GetRequiredService<MainContext>();
            return dbContext;
        }
    }
}
