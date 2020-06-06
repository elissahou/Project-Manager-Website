using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace E9U.Tangello.Private.Pages
{
    public class InUseProjectModel : PageModel
    {
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public string Category { get; set; }
        public string ProjectType { get; set; }

        private IRepository Repository { get; }

        public InUseProjectModel(IRepository repository)
        {
            this.Repository = repository;
        }

        public async Task OnGet([FromQuery] string projectName)
        {
            this.ProjectName = projectName;
            this.ProjectDescription = await this.Repository.GetProjectDescriptionAsync(projectName);
            this.ProjectDescription = this.ProjectDescription ?? string.Empty;

            this.Category = await this.Repository.GetCategoryOfProjectNameAsync(projectName);
            this.ProjectType = await this.Repository.GetProjectTypeOfCategoryAsync(this.Category);
        }

        public async Task<IActionResult> OnPostSaveChangesAsync([FromQuery] string projectName, [FromForm] string description)
        {
            await this.Repository.ChangeProjectDescriptionAsync(projectName, description);

            return new OkResult();
        }

        public async Task<IActionResult> OnPostDeleteProjectAsync([FromQuery] string projectName)
        {
            await this.Repository.MakeInUseProjectAvailableAsync(projectName);
            this.Category = await this.Repository.GetCategoryOfProjectNameAsync(projectName);

            var categoryName = new { categoryName = this.Category };

            return new OkObjectResult(categoryName);
        }
    }
}