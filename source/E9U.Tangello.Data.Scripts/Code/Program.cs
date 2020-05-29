using System;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Microsoft.EntityFrameworkCore;

namespace E9U.Tangello.Data.Scripts
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Tests.SubMain();
        }

        public static IServiceProvider GetServiceProvider()
        {
            // Note that this is repeated in Migrations (under DesignTimeDbContextFactory).
            // However, this is okay, because every application should configure its own services.
            var serviceCollection = new ServiceCollection()
                .AddMainContext()
                .AddLogging(loggingBuilder =>
                {
                    loggingBuilder
                        .AddConsole()
                        //.SetMinimumLevel(LogLevel.Debug)
                        ;
                })
                ;

            var serviceProvider = serviceCollection.BuildServiceProvider();
            return serviceProvider;
        }

        public static MainDbContext GetMainDbContext()
        {
            var serviceProvider = Program.GetServiceProvider();

            var mainDbContext = GetMainDbContext(serviceProvider);
            return mainDbContext;
        }

        public static MainDbContext GetMainDbContext(IServiceProvider serviceProvider)
        {
            var mainDbContext = serviceProvider.GetRequiredService<MainDbContext>();
            return mainDbContext;
        }

        public static DbContextOptions<MainDbContext> GetMainDbContextOptions(IServiceProvider serviceProvider)
        {
            var mainDbContextOptions = serviceProvider.GetRequiredService<DbContextOptions<MainDbContext>>();
            return mainDbContextOptions;
        }

        public static DbContextOptions<MainDbContext> GetMainDbContextOptions()
        {
            var serviceProvider = Program.GetServiceProvider();

            var mainDbContextOptions = Program.GetMainDbContextOptions(serviceProvider);
            return mainDbContextOptions;
        }

        public static MainDbContext GetNewMainDbContextInstance(IServiceProvider serviceProvider)
        {
            var mainDbContextOptions = Program.GetMainDbContextOptions(serviceProvider);

            var mainDbContext = Program.GetNewMainDbContextInstance(mainDbContextOptions);
            return mainDbContext;
        }

        public static MainDbContext GetNewMainDbContextInstance(DbContextOptions<MainDbContext> mainDbContextOptions)
        {
            var mainDbContext = new MainDbContext(mainDbContextOptions);
            return mainDbContext;
        }
    }
}
