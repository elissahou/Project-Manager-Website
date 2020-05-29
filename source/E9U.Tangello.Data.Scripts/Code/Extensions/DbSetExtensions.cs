using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;


namespace E9U.Tangello.Data.Scripts
{
    public static class DbSetExtensions
    {
        public static void RemoveAll<T>(this DbSet<T> dbSet)
            where T : class
        {
            foreach (var entity in dbSet)
            {
                dbSet.Remove(entity);
            }
        }

        public static async Task RemoveAllAsync<T>(this DbSet<T> dbSet)
            where T : class
        {
            var entities = await dbSet.ToListAsync();
            foreach (var entity in entities)
            {
                dbSet.Remove(entity);
            }
        }
    }
}
