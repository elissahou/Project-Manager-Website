using System;

using Microsoft.Extensions.DependencyInjection;

using Microsoft.EntityFrameworkCore;


namespace E9U.Tangello.Data
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddMainContext(this IServiceCollection services)
        {
            services
                .AddDbContext<MainDbContext>(dbContextOptionsBuilder =>
                {
                    var connectionString = ConnectionString.LocalDB;
                    dbContextOptionsBuilder.UseSqlServer(connectionString, sqlServerDbContextOptionsBuilder =>
                    {
                        sqlServerDbContextOptionsBuilder.MigrationsAssembly("E9U.Tangello.Data.Migrations");
                    });
                })
                ;

            return services;
        }
    }
}
