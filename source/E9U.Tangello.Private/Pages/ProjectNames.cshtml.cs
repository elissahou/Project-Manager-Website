using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace E9U.Tangello.Private.Pages
{
    // [IgnoreAntiforgeryToken(Order=1001)] // delete later
    public class ProjectNamesModel : PageModel
    {
        public string CategoryName { get; set; }

        public List<string> AvailableProjectNames { get; set; } = new List<string>();
        public List<string> InUseProjectNames { get; set; } = new List<string>();

        private IRepository Repository { get; }


        public ProjectNamesModel(IRepository repository)
        {
            this.Repository = repository;
        }

        public async Task OnGetAsync([FromQuery] string categoryName)
        {
            this.CategoryName = categoryName;

            await this.InitializeProjectNamesListsAsync();
        }

        public async Task<IActionResult> OnPostGetProjectNameAsync([FromQuery] string categoryName)
        {
            this.CategoryName = categoryName;
            await this.InitializeProjectNamesListsAsync();

            if (!this.AvailableProjectNames.Any())
            {

            }

            // choosing random project name
            var projectName = this.AvailableProjectNames.First();

            await this.Repository.MarkProjectNameAsInUseAsync(projectName);

            return this.RedirectToPage(new { categoryName });
        }


        public async Task<IActionResult> OnPostGetProjectName2Async([FromQuery] string categoryName)
        {
            this.CategoryName = categoryName;
            await this.InitializeProjectNamesListsAsync();

            if (!this.AvailableProjectNames.Any())
            {

            }

            // choosing random project name
            var projectName = this.AvailableProjectNames.First();

            await this.Repository.MarkProjectNameAsInUseAsync(projectName);

            var projectNameAsMoreComplicatedObject = new { ProjectName = projectName};

            return new OkObjectResult(projectNameAsMoreComplicatedObject);
        }

        public async Task<IActionResult> OnPostAsync([FromForm] string newProjectName, [FromQuery] string categoryName)
        {
            await this.Repository.PostProjectNamesForCategoryAsync(categoryName, newProjectName);

            return this.RedirectToPage(new { categoryName });
        }

        private async Task InitializeProjectNamesListsAsync()
        {
            this.AvailableProjectNames = (await this.Repository.GetAvailableProjectNamesForCategoryAsync(this.CategoryName)).ToList();
            this.AvailableProjectNames.Sort();

            this.InUseProjectNames = (await this.Repository.GetInUseProjectNamesForCategoryAsync(this.CategoryName)).ToList();
            this.InUseProjectNames.Sort();
        }
    }
}