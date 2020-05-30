using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace E9U.Tangello.Private.Pages
{
    public class ProjectNamesModel : PageModel
    {
        public string CategoryName { get; set; }
        public List<string> ProjectNames { get; set; } = new List<string>();
        private IRepository Repository { get; }


        public ProjectNamesModel(IRepository repository)
        {
            this.Repository = repository;
        }

        public async Task OnGetAsync([FromQuery] string categoryName)
        {
            this.CategoryName = categoryName;

            var projectNames = await this.Repository.GetAllProjectNamesForCategoryAsync(categoryName);

            this.ProjectNames = projectNames.ToList();
            this.ProjectNames.Sort();
        }

        public async Task<IActionResult> OnPostAsync([FromForm] string newProjectName, [FromQuery] string categoryName)
        {
            await this.Repository.PostProjectNamesForCategoryAsync(categoryName, newProjectName);

            return this.RedirectToPage(new { categoryName });
        }
    }
}