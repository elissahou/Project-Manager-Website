using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using Microsoft.EntityFrameworkCore;

using E9U.Tangello.Data.Entities;


namespace E9U.Tangello.Data.Scripts
{
    public class Tests
    {
        public static async Task SubMain()
        {
            var mainDbContextOptions = Program.GetMainDbContextOptions();
            var mainDbContext = Program.GetMainDbContext();

            // Tests.AddCategoryTest(mainDbContext);
            // Tests.ResetDatabaseTables(mainDbContext);
            //await Tests.ResetDatabaseTablesAsync(mainDbContext);
            //await Tests.ResetDatabaseTablesAsyncParallel(mainDbContext); // This crashes, DbContext is *NOT* thread-safe.
            await Tests.ResetDatabaseTablesAsyncParallel(mainDbContextOptions);
            //Tests.AddEntityToDatabaseTables01(mainDbContext);
            Tests.AddEntityToDatabaseTables02(mainDbContext);
        }

        static void AddCategoryTest(MainDbContext mainDbContext)
        {
            var categoryStarbucksDrinks = new Category
            {
                Name = "Starbucks Drinks"
            };

            mainDbContext.Categories.Add(categoryStarbucksDrinks);

            mainDbContext.SaveChanges();
        }

        static void AddEntityToDatabaseTables01(MainDbContext mainDbContext)
        {
            var categoryStarbucksDrinks = new Category
            {
                Name = "Starbucks Drinks"
            };

            var projectNameFlatWhite = new ProjectName
            {
                Name = "Flat White"
            };

            var projectTypeExperiments = new ProjectType
            {
                Name = "Experiments"
            };

            mainDbContext.Categories.Add(categoryStarbucksDrinks);
            mainDbContext.ProjectNames.Add(projectNameFlatWhite);
            mainDbContext.ProjectTypes.Add(projectTypeExperiments);

            mainDbContext.SaveChanges();

            var categoryToProjectTypeMapping_StarbucksDrinksToExperiments = new CategoryToProjectTypeMapping
            {
                CategoryID = categoryStarbucksDrinks.ID,
                ProjectTypeID = projectTypeExperiments.ID,
                StartDate = DateTime.Now,
                EndDate = DateTime.MaxValue
            };

            var projectNameToCategoryMapping_FlatWhiteToStarbucksDrinks = new ProjectNameToCategoryMapping
            {
                ProjectNameID = projectNameFlatWhite.ID,
                CategoryID = categoryStarbucksDrinks.ID,
                StartDate = DateTime.Now,
                EndDate = DateTime.MaxValue,
            };

            mainDbContext.CategoryToProjectTypeMappings.Add(categoryToProjectTypeMapping_StarbucksDrinksToExperiments);
            mainDbContext.ProjectNameToCategoryMappings.Add(projectNameToCategoryMapping_FlatWhiteToStarbucksDrinks);

            mainDbContext.SaveChanges();
        }

        static void AddEntityToDatabaseTables02(MainDbContext mainDbContext)
        {
            var categoryStarbucksDrinks = new Category
            {
                Name = "Starbucks Drinks"
            };

            var projectNameFlatWhite = new ProjectName
            {
                Name = "Flat White"
            };

            var projectTypeExperiments = new ProjectType
            {
                Name = "Experiments"
            };

            var categoryToProjectTypeMapping_StarbucksDrinksToExperiments = new CategoryToProjectTypeMapping
            {
                Category = categoryStarbucksDrinks,
                ProjectType = projectTypeExperiments,
                StartDate = DateTime.Now,
                EndDate = DateTime.MaxValue
            };

            var projectNameToCategoryMapping_FlatWhiteToStarbucksDrinks = new ProjectNameToCategoryMapping
            {
                ProjectName = projectNameFlatWhite,
                Category = categoryStarbucksDrinks,
                StartDate = DateTime.Now,
                EndDate = DateTime.MaxValue,
            };

            mainDbContext.CategoryToProjectTypeMappings.Add(categoryToProjectTypeMapping_StarbucksDrinksToExperiments);
            mainDbContext.ProjectNameToCategoryMappings.Add(projectNameToCategoryMapping_FlatWhiteToStarbucksDrinks);

            mainDbContext.SaveChanges();
        }

        static void ResetDatabaseTables(MainDbContext mainDbContext)
        {
            Tests.ConfirmDatabaseReset();

            mainDbContext.ClearAll<Category>();
            mainDbContext.ClearAll<CategoryToProjectTypeMapping>();
            mainDbContext.ClearAll<ProjectName>();
            mainDbContext.ClearAll<ProjectNameToCategoryMapping>();
            mainDbContext.ClearAll<ProjectType>();
        }

        static async Task ResetDatabaseTablesAsync(MainDbContext mainDbContext)
        {
            Tests.ConfirmDatabaseReset();

            await mainDbContext.ClearAllAsync<Category>();
            await mainDbContext.ClearAllAsync<CategoryToProjectTypeMapping>();
            await mainDbContext.ClearAllAsync<ProjectName>();
            await mainDbContext.ClearAllAsync<ProjectNameToCategoryMapping>();
            await mainDbContext.ClearAllAsync<ProjectType>();
        }

        /// <summary>
        /// Note: this crashes!
        /// </summary>
        static Task ResetDatabaseTablesAsyncParallel(MainDbContext mainDbContext)
        {
            Tests.ConfirmDatabaseReset();

            var clearingCategories = mainDbContext.ClearAllAsync<Category>();
            var clearingCategoryMappings = mainDbContext.ClearAllAsync<CategoryToProjectTypeMapping>();
            var clearingProjectNames = mainDbContext.ClearAllAsync<ProjectName>();
            var clearingProjectNameMappings = mainDbContext.ClearAllAsync<ProjectNameToCategoryMapping>();
            var clearingProjectTypes = mainDbContext.ClearAllAsync<ProjectType>();

            var task = Task.WhenAll(clearingCategories, clearingCategoryMappings, clearingProjectNames, clearingProjectTypes, clearingProjectNameMappings);
            return task;
        }

        static Task ResetDatabaseTablesAsyncParallel(DbContextOptions<MainDbContext> mainDbContextOptions)
        {
            Tests.ConfirmDatabaseReset();

            var clearingCategories = Program.GetNewMainDbContextInstance(mainDbContextOptions).ClearAllAsync<Category>();
            var clearingCategoryMappings = Program.GetNewMainDbContextInstance(mainDbContextOptions).ClearAllAsync<CategoryToProjectTypeMapping>();
            var clearingProjectNames = Program.GetNewMainDbContextInstance(mainDbContextOptions).ClearAllAsync<ProjectName>();
            var clearingProjectNameMappings = Program.GetNewMainDbContextInstance(mainDbContextOptions).ClearAllAsync<ProjectNameToCategoryMapping>();
            var clearingProjectTypes = Program.GetNewMainDbContextInstance(mainDbContextOptions).ClearAllAsync<ProjectType>();

            var task = Task.WhenAll(clearingCategories, clearingCategoryMappings, clearingProjectNames, clearingProjectTypes, clearingProjectNameMappings);
            return task;
        }

        static void ConfirmDatabaseReset()
        {
            Console.WriteLine("Are you sure you want to reset the database?");
            Console.WriteLine("Hit Enter to clear the database. Stop the program to abort.");
            Console.ReadLine();
        }
    }
}
