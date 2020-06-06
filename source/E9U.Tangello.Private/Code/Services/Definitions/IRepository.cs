using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace E9U.Tangello.Private
{
    public interface IRepository
    {
        Task<IEnumerable<string>> GetAllProjectNamesForCategoryAsync(string category);

        Task<IEnumerable<string>> GetInUseProjectNamesForCategoryAsync(string category);

        Task<IEnumerable<string>> GetAvailableProjectNamesForCategoryAsync(string category);

        Task<(IEnumerable<string> available, IEnumerable<string> inUse)> GetAvailableAndInUseProjectNamesForCategoryAsync(string category);


        Task AddProjectNameForCategoryAsync(string category, string projectName);

        Task MarkProjectNameAsInUseAsync(string projectName);

        Task AddNewCategoryAsync(string category);



        Task<IEnumerable<string>> GetAllCategoriesAsync();

        Task<IEnumerable<string>> GetAssignedCategoriesAsync();

        /// <summary>
        /// Get categories that are unassigned to a project type.
        /// </summary>
        Task<IEnumerable<string>> GetUnassignedCategoriesAsync();


        Task AssignCategoryToProjectTypeAsync(string category, string projectType);

        Task<string> GetProjectTypeOfCategoryAsync(string assignedCategory);

        Task DeleteEverythingForCategoryAsync(string category);

        Task RenameProjectTypeForOneCategoryOnlyAsync(string category, string oldProjectTypeName, string newProjectTypeName);



        Task<string> GetProjectDescriptionAsync(string projectName);

        Task ChangeProjectDescriptionAsync(string projectName, string description);

        Task<string> GetCategoryOfProjectNameAsync(string projectName);

        Task MakeInUseProjectAvailableAsync(string projectName);
    }
}
