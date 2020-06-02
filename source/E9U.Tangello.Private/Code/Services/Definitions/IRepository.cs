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

        Task PostProjectNamesForCategoryAsync(string category, string projectName);

        Task MarkProjectNameAsInUseAsync(string projectName);
    }
}
