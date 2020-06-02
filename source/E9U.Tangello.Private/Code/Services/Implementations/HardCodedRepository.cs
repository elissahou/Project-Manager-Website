using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace E9U.Tangello.Private
{
    //public class HardCodedRepository : IRepository // Removed implementaiton for now, so this file won't need to be updated as IRepository is updated.
    public class HardCodedRepository
    {
        public Task<IEnumerable<string>> GetAllProjectNamesForCategoryAsync(string category)
        {
            var projectNames = new List<string>()
            {
                "Tangerine",
                "Lemon",
                "Pamplemousse",
                "Raspberry"
            } as IEnumerable<string>;

            return Task.FromResult(projectNames);
        }

        public Task<IEnumerable<string>> GetAvailableProjectNamesForCategoryAsync(string category)
        {
            throw new NotImplementedException();
        }

        public Task PostProjectNamesForCategoryAsync(string category, string projectName)
        {
            throw new NotImplementedException();
        }
    }
}
