using System;

using Microsoft.EntityFrameworkCore;

using E9U.Tangello.Data.Entities;


namespace E9U.Tangello.Data
{
    public class MainContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryToProjectTypeMapping> CategoryToProjectTypeMappings { get; set; }
        public DbSet<ProjectName> ProjectNames { get; set; }
        public DbSet<ProjectNameToCategoryMapping> ProjectNameToCategoryMappings { get; set; }
        public DbSet<ProjectType> ProjectTypes { get; set; }
    }
}
