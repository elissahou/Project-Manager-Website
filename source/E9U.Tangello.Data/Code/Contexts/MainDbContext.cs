using System;

using Microsoft.EntityFrameworkCore;

using E9U.Tangello.Data.Entities;


namespace E9U.Tangello.Data
{
    public class MainDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryToProjectTypeMapping> CategoryToProjectTypeMappings { get; set; }
        public DbSet<ProjectName> ProjectNames { get; set; }
        public DbSet<ProjectNameToCategoryMapping> ProjectNameToCategoryMappings { get; set; }
        public DbSet<ProjectType> ProjectTypes { get; set; }


        public MainDbContext(DbContextOptions<MainDbContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Atoms
            modelBuilder.Entity<Category>().HasAlternateKey(c => c.Name);
            modelBuilder.Entity<ProjectName>().HasAlternateKey(p => p.Name);
            modelBuilder.Entity<ProjectType>().HasAlternateKey(p => p.Name);
        }
    }
}
