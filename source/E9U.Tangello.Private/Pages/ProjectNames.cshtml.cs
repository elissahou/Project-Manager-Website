using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace E9U.Tangello.Private.Pages
{
    // [IgnoreAntiforgeryToken(Order=1001)]
    public class ProjectNamesModel : PageModel
    {
        public string CategoryName { get; set; }
        public string ProjectType { get; set; }

        public List<string> AvailableProjectNames { get; set; } = new List<string>();
        public List<string> InUseProjectNames { get; set; } = new List<string>();

        private IRepository Repository { get; }


        public ProjectNamesModel(IRepository repository)
        {
            this.Repository = repository;
        }

        public async Task<IActionResult> OnGetAsync([FromQuery] string categoryName)
        {
            this.CategoryName = categoryName;
            this.ProjectType = await this.Repository.GetProjectTypeOfCategoryAsync(categoryName);

            if (this.ProjectType == default)
            {
                return this.RedirectToPage("Error");
            }

            await this.InitializeProjectNamesListsAsync();
            return this.Page();
        }

        public async Task<IActionResult> OnPostGetAvailableProjectNameAsync([FromForm] string projectNameChosen, [FromForm] string getProjectNameOption, [FromQuery] string categoryName)
        {
            this.CategoryName = categoryName;
            await this.InitializeProjectNamesListsAsync();

            // Check for input validity
            if ( !(this.AvailableProjectNames.Contains(projectNameChosen)) )
            {
                return this.RedirectToPage("Error");
            }

            string projectName;

            // choosing first available project name
            if (getProjectNameOption == "first")
            {
                projectName = this.AvailableProjectNames.First();
            } else {
                projectName = projectNameChosen;
            }
            
            await this.Repository.MarkProjectNameAsInUseAsync(projectName);

            var projectNameAsMoreComplicatedObject = new { ProjectName = projectName};

            return new OkObjectResult(projectNameAsMoreComplicatedObject);
        }

        public async Task<IActionResult> OnPostDeleteCategoryAsync([FromQuery] string categoryName)
        {
            await this.Repository.DeleteEverythingForCategoryAsync(categoryName);

            var category = new { categoryString = categoryName };

            return new OkObjectResult(category);
        }

        public async Task<IActionResult> OnPostRenameProjectTypeForOneCategoryOnlyAsync([FromQuery] string categoryName, [FromForm] string inputProjectType)
        {
            this.CategoryName = categoryName;
            this.ProjectType = await this.Repository.GetProjectTypeOfCategoryAsync(categoryName);

            await this.Repository.RenameProjectTypeForOneCategoryOnlyAsync(this.CategoryName, this.ProjectType, inputProjectType);

            var newProjectType = new { projectType = inputProjectType };

            return new OkObjectResult(newProjectType);
        }

        public async Task<IActionResult> OnPostAsync([FromForm] string newProjectName, [FromQuery] string categoryName)
        {
            this.CategoryName = categoryName;
            await this.InitializeProjectNamesListsAsync();

            if (this.InUseProjectNames.Contains(newProjectName) || this.AvailableProjectNames.Contains(newProjectName))
            {
                return this.RedirectToPage("Error");
            }

            await this.Repository.AddProjectNameForCategoryAsync(categoryName, newProjectName);

            return this.RedirectToPage(new { categoryName });
        }

        private async Task InitializeProjectNamesListsAsync()
        {
            (IEnumerable<string> available, IEnumerable<string> inUse) = await this.Repository.GetAvailableAndInUseProjectNamesForCategoryAsync(this.CategoryName);

            this.AvailableProjectNames = (available).ToList();
            this.AvailableProjectNames.Sort();

            this.InUseProjectNames = (inUse).ToList();
            this.InUseProjectNames.Sort();
        }
    }
}