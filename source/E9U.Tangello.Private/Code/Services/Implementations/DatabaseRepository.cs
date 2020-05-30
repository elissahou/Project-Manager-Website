using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using E9U.Tangello.Data;
using E9U.Tangello.Data.Entities;


namespace E9U.Tangello.Private
{
    public class DatabaseRepository : IRepository
    {
        private MainDbContext MainDbContext { get; }


        public DatabaseRepository(MainDbContext mainDbContext)
        {
            this.MainDbContext = mainDbContext;
        }

        public async Task<IEnumerable<string>> GetAllProjectNamesForCategoryAsync(string category)
        {
            //var projectNames = await this.MainDbContext.Categories // Start with the category entities.
            //    .Where(x => x.Name == category)
            //    .Join(this.MainDbContext.ProjectNameToCategoryMappings, // Join it to the mapping entities.
            //        categoryEntity => categoryEntity.ID,
            //        mappingEntity => mappingEntity.CategoryID,
            //        (categoryEntity2, mappingEntity2) => mappingEntity2
            //        )
            //    .Join(this.MainDbContext.ProjectNames,
            //        x => x.ProjectNameID,
            //        x => x.ID,
            //        (x, y) => y)
            //    .Select(x => x.Name)
            //    .ToListAsync();

            //return projectNames;

            var projectNames = await
                (from x in this.MainDbContext.Categories
                    join y in this.MainDbContext.ProjectNameToCategoryMappings
                        on x.ID equals y.CategoryID
                    join z in this.MainDbContext.ProjectNames
                        on y.ProjectNameID equals z.ID
                    where x.Name == category
                    select z.Name)
                .ToListAsync();

            return projectNames;

            // x, y, and z are dummy variables.
            // x: Category entities
            // y: ProjectNameToCategoryMapping entities
            // z: ProjectName entities


            //// Get the category entity (there should only be one).
            //var categoryEntity = await this.MainDbContext.Categories
            //    .Where(x => x.Name == category)
            //    .SingleAsync();

            //// Get the project name to category mappings.
            //var projectNameIDsForCategory = await this.MainDbContext.ProjectNameToCategoryMappings
            //    .Where(x => x.CategoryID == categoryEntity.ID)
            //    .Select(x => x.ProjectNameID)
            //    .ToListAsync();

            //// Get project names.
            //var projectNames = await this.MainDbContext.ProjectNames
            //        .Where(x => projectNameIDsForCategory.Contains(x.ID))
            //        .Select(x => x.Name)
            //        .ToListAsync();

            //return projectNames;
        }

        public async Task PostProjectNamesForCategoryAsync(string category, string projectName)
        {
                var categoryEntity = await this.MainDbContext.Categories
                    .Where(x => x.Name == category)
                    .SingleAsync()
                    ;

                var projectNameEntity = new ProjectName
                {
                    Name = projectName
                };

                var projectNameToCategoryMapping = new ProjectNameToCategoryMapping
                {
                    ProjectName = projectNameEntity,
                    Category = categoryEntity,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.MaxValue,
                };

                this.MainDbContext.ProjectNameToCategoryMappings.Add(projectNameToCategoryMapping);
                await this.MainDbContext.SaveChangesAsync();
        }

        public Task<IEnumerable<string>> GetAvailableProjectNamesForCategoryAsync(string category)
        {
            //var inUseProjectNames = await
            //    (from x in this.MainDbContext.Categories
            //     join y in this.MainDbContext.ProjectNameToCategoryMappings
            //         on x.ID equals y.CategoryID
            //     join z in this.MainDbContext.ProjectNames
            //         on y.ProjectNameID equals z.ID
            //     where x.Name == category
            //     select z.Name)
            //    .ToListAsync();

            //var allProjectNames = this.GetAllProjectNamesForCategoryAsync(category);


            throw new NotImplementedException();
        }
    }
}