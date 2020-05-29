using System;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;


namespace E9U.Tangello.Data.Scripts
{
    public static class DbContextExtensions
    {
        public static void ClearAll<T>(this DbContext dbContext)
            where T : class
        {
            var dbSet = dbContext.Set<T>();

            dbSet.RemoveAll();

            dbContext.SaveChanges();
        }

        public static async Task ClearAllAsync<T>(this DbContext dbContext)
            where T : class
        {
            var dbSet = dbContext.Set<T>();

            await dbSet.RemoveAllAsync();

            await dbContext.SaveChangesAsync();
        }
    }
}
