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

        #region ProjectNames

        public IQueryable<ProjectName> GetAllProjectNamesForCategoryQuery(string category)
        {
            var getAllProjectNamesForCategoryQuery =
                from x in this.MainDbContext.Categories
                join y in this.MainDbContext.ProjectNameToCategoryMappings
                    on x.ID equals y.CategoryID
                join z in this.MainDbContext.ProjectNames
                    on y.ProjectNameID equals z.ID
                where x.Name == category
                select z;

            return getAllProjectNamesForCategoryQuery;
        }

        public async Task<IEnumerable<string>> GetAllProjectNamesForCategoryAsync(string category)
        {
            var getAllProjectNamesForCategoryQuery = this.GetAllProjectNamesForCategoryQuery(category);

            var projectNames = await getAllProjectNamesForCategoryQuery
                .Select(x => x.Name)
                .ToListAsync();

            return projectNames;
        }

        private IQueryable<ProjectName> GetInUseProjectNamesForCategoryQuery(string category)
        {
            var allProjectNamesForCategoryQuery = this.GetAllProjectNamesForCategoryQuery(category);

            var inUseProjectNamesForCategory =
                from x in this.MainDbContext.InUseProjectNames
                join y in allProjectNamesForCategoryQuery
                   on x.ProjectNameID equals y.ID
                select y;

            return inUseProjectNamesForCategory;
        }

        public async Task<IEnumerable<string>> GetInUseProjectNamesForCategoryAsync(string category)
        {
            var inUseProjectNamesForCategoryQuery = this.GetInUseProjectNamesForCategoryQuery(category);

            var inUseProjectNamesForCategory = await inUseProjectNamesForCategoryQuery
                .Select(x => x.Name)
                .ToListAsync();

            return inUseProjectNamesForCategory;
        }

        public async Task<IEnumerable<string>> GetAvailableProjectNamesForCategoryAsync(string category)
        {
            var allProjectNamesForCategoryQuery = this.GetAllProjectNamesForCategoryQuery(category);
            var inUseProjectNamesForCategoryQuery = this.GetInUseProjectNamesForCategoryQuery(category);

            var availableProjectNamesQuery = allProjectNamesForCategoryQuery
                .Where(x => !inUseProjectNamesForCategoryQuery.Contains(x));

            var availableProjectNames = (await availableProjectNamesQuery.ToListAsync())
                .Select(x => x.Name);

            return availableProjectNames;
        }

        public async Task<(IEnumerable<string> available, IEnumerable<string> inUse)> GetAvailableAndInUseProjectNamesForCategoryAsync(string category)
        {
            var allProjectNamesForCategoryQuery = this.GetAllProjectNamesForCategoryQuery(category);
            var inUseProjectNamesForCategoryQuery = this.GetInUseProjectNamesForCategoryQuery(category);

            var query =
                from x in allProjectNamesForCategoryQuery
                join y in inUseProjectNamesForCategoryQuery
                    on x.ID equals y.ID into temp
                from p in temp.DefaultIfEmpty()
                select new { X = x, InUse = p != null };

            var results = await query.ToListAsync();

            var availableResults = results.Where(x => x.InUse == false).Select(x => x.X.Name);
            var inUseResults = results.Where(x => x.InUse == true).Select(x => x.X.Name);

            return (available: availableResults, inUse: inUseResults);
        }

        public async Task AddProjectNameForCategoryAsync(string category, string projectName)
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

        /// <summary>
        /// Random choose a new project from ProjectNames, and creates for it an InUseProjectName.
        /// </summary>
        public async Task MarkProjectNameAsInUseAsync(string projectNameString)
        {
            var projectNameEntity = this.MainDbContext.ProjectNames
                .Where(x => x.Name == projectNameString)
                .Single()
                ;

            var inUseProjectName = new InUseProjectName
            {
                ProjectName = projectNameEntity,
                StartDate = DateTime.Now,
            };

            this.MainDbContext.InUseProjectNames.Add(inUseProjectName);

            await this.MainDbContext.SaveChangesAsync();
        }

        #endregion

        #region Categories
        public async Task<IEnumerable<string>> GetAllCategoriesAsync()
        {
            var categories = await
                (from x in this.MainDbContext.Categories
                 select x.Name)
                 .ToListAsync();

            return categories;
        }

        public async Task<IEnumerable<string>> GetAssignedCategoriesAsync()
        {
            var assignedCategories = await
                (from x in this.MainDbContext.CategoryToProjectTypeMappings
                 select x.Category.Name)
                 .ToListAsync()
                 ;

            return assignedCategories;
        }

        public async Task<IEnumerable<string>> GetUnassignedCategoriesAsync()
        {
            var allCategories = await this.GetAllCategoriesAsync();
            var assignedCategories = await this.GetAssignedCategoriesAsync();

            var unassignedCategories = allCategories.Except(assignedCategories);
            return unassignedCategories;
        }

        public async Task AddNewCategoryAsync(string category)
        {
            var categoryEntity = new Category
            {
                Name = category,
            };
            
            this.MainDbContext.Categories.Add(categoryEntity);
            await this.MainDbContext.SaveChangesAsync();
        }


        public async Task AssignCategoryToProjectTypeAsync(string category, string projectType)
        {
            var categoryEntity = await this.MainDbContext.Categories
                .Where(x => x.Name == category)
                .SingleAsync()
                ;

            var projectTypeEntity = await this.MainDbContext.ProjectTypes
                .Where(x => x.Name == projectType)
                .SingleOrDefaultAsync()
                ;


            var projectTypeEntityNew = new ProjectType
            {
                Name = projectType
            };

            var projectTypeEntityToBeMapped = projectTypeEntity ?? projectTypeEntityNew;

            var newCategoryToProjectTypeMapping = new CategoryToProjectTypeMapping
            {
                Category = categoryEntity,
                ProjectType = projectTypeEntityToBeMapped
            };

            this.MainDbContext.CategoryToProjectTypeMappings.Add(newCategoryToProjectTypeMapping);
            await this.MainDbContext.SaveChangesAsync();
        }

        public async Task RenameProjectTypeForOneCategoryOnlyAsync(string category, string oldProjectTypeName, string newProjectTypeName)
        {
            var oldCategoryToProjectTypeMapping = await this.MainDbContext.CategoryToProjectTypeMappings
                .Where(x => x.Category.Name == category)
                .Where(x => x.ProjectType.Name == oldProjectTypeName)
                .SingleAsync()
                ;

            this.MainDbContext.CategoryToProjectTypeMappings.Remove(oldCategoryToProjectTypeMapping);

            await this.AssignCategoryToProjectTypeAsync(category, newProjectTypeName);
        }

        public async Task<string> GetProjectTypeOfCategoryAsync(string assignedCategory)
        {
            var projectType = await (from x in this.MainDbContext.CategoryToProjectTypeMappings
                                     where x.Category.Name == assignedCategory
                                     select x.ProjectType.Name)
                                    .SingleOrDefaultAsync();
                                    ;

            return projectType;
        }

        public async Task DeleteEverythingForCategoryAsync(string category)
        {
            var projectNameEntities = 
                (from x in this.MainDbContext.Categories
                 join y in this.MainDbContext.ProjectNameToCategoryMappings
                     on x.ID equals y.CategoryID
                 join z in this.MainDbContext.ProjectNames
                     on y.ProjectNameID equals z.ID
                 where x.Name == category
                 select z);

            foreach (var projectNameEntity in projectNameEntities)
            {
                var projectNameString = projectNameEntity.Name;

                // Delete the InUseProject entities.
                if (this.MainDbContext.InUseProjectNames.Any(x => x.ProjectName.Name == projectNameString)) {
                    await this.MakeInUseProjectAvailableAsync(projectNameString);
                }

                // Delete the ProjectNameToCategoryMapping entities.
                await this.DeleteProjectNameToCategoryMapping(projectNameEntity);

                // Delete the ProjectName entities.
                await this.DeleteProjectNameAsync(projectNameEntity);
            }

            // Delete the CategoryToProjectTypeMapping entities.
            await this.DeleteCategoryToProjectTypeMappingAsync(category);

            // Delete the Category entity.
            await this.DeleteOnlyCategoryEntityAsync(category);
        }

        public async Task DeleteCategoryToProjectTypeMappingAsync(string category)
        {
            var categoryToProjectTypeMappingEntities = (from x in this.MainDbContext.CategoryToProjectTypeMappings
                                                            where x.Category.Name == category
                                                            select x)
                                                            ;

            foreach (var categoryToProjectTypeMappingEntity in categoryToProjectTypeMappingEntities)
            {
                this.MainDbContext.CategoryToProjectTypeMappings.Remove(categoryToProjectTypeMappingEntity);
            }
            
            await this.MainDbContext.SaveChangesAsync();
        }

        public async Task DeleteProjectNameAsync(ProjectName projectNameEntity)
        {
            this.MainDbContext.ProjectNames.Remove(projectNameEntity);
            await this.MainDbContext.SaveChangesAsync();
        }

        public async Task DeleteProjectNameToCategoryMapping(ProjectName projectNameEntity)
        {
            var projectNameToCategoryMappingEntity = await (from x in this.MainDbContext.ProjectNameToCategoryMappings
                                                      where x.ProjectName == projectNameEntity
                                                      select x)
                                 .SingleOrDefaultAsync()
                                 ;

            this.MainDbContext.ProjectNameToCategoryMappings.Remove(projectNameToCategoryMappingEntity);
            await this.MainDbContext.SaveChangesAsync();

        }

        public async Task DeleteOnlyCategoryEntityAsync(string category)
        {
            var categoryEntity = await (from x in this.MainDbContext.Categories
                                        where x.Name == category
                                        select x)
                                 .SingleOrDefaultAsync()
                                 ;

            this.MainDbContext.Categories.Remove(categoryEntity);
            await this.MainDbContext.SaveChangesAsync();

        }

        #endregion

        #region InUseProjectName

        public async Task<string> GetProjectDescriptionAsync(string projectName)
        {
            var description = await
                (from x in this.MainDbContext.InUseProjectNames
                 where x.ProjectName.Name == projectName
                 select x.ProjectDescription)
                 .SingleOrDefaultAsync()
                 ;

            return description;
        }

        public async Task ChangeProjectDescriptionAsync(string projectName, string description)
        {
            var inUseProjectEntity = await this.GetInUseProjectEntityFromProjectNameStringAsync(projectName);

            inUseProjectEntity.ProjectDescription = description;
            await this.MainDbContext.SaveChangesAsync();
        }

        public async Task<string> GetCategoryOfProjectNameAsync(string projectName)
        {
            var category = await
                (from x in this.MainDbContext.ProjectNameToCategoryMappings
                 where x.ProjectName.Name == projectName
                 select x.Category.Name)
                 .SingleOrDefaultAsync()
                 ;

            return category;
        }

        public async Task MakeInUseProjectAvailableAsync(string projectName)
        {
            var inUseProjectEntity = await this.GetInUseProjectEntityFromProjectNameStringAsync(projectName);

            this.MainDbContext.InUseProjectNames.Remove(inUseProjectEntity);
            await this.MainDbContext.SaveChangesAsync();
        }

        public async Task<InUseProjectName> GetInUseProjectEntityFromProjectNameStringAsync(string projectName)
        {
            var inUseProjectEntity = await this.MainDbContext.InUseProjectNames
                .Where(x => x.ProjectName.Name == projectName)
                .SingleOrDefaultAsync()
                ;

            return inUseProjectEntity;
        }

        #endregion
    }
}