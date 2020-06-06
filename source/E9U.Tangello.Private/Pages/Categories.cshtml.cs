using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace E9U.Tangello.Private.Pages
{
    public class CategoriesModel : PageModel
    {
        public List<string> ProjectTypes { get; set; } = new List<string>();
        public List<string> AssignedCategories { get; set; } = new List<string>();
        public List<string> UnassignedCategories { get; set; } = new List<string>();

        private IRepository Repository { get; }


        public CategoriesModel(IRepository repository)
        {
            this.Repository = repository;
        }

        public async Task OnGetAsync()
        {
            await this.InitializeProjectNamesListsAsync();
        }

        private async Task InitializeProjectNamesListsAsync()
        {
            this.AssignedCategories = (await this.Repository.GetAssignedCategoriesAsync()).ToList();
            this.AssignedCategories.Sort();
            foreach (var assignedCategory in AssignedCategories)
            {
                var projectType = await this.Repository.GetProjectTypeOfCategoryAsync(assignedCategory);
                this.ProjectTypes.Add(projectType);
            }

            this.UnassignedCategories = (await this.Repository.GetUnassignedCategoriesAsync()).ToList();
            this.UnassignedCategories.Sort();
        }

        public async Task<IActionResult> OnPostAssignAsync([FromForm] string category, [FromForm] string inputProjectType)
        {
            await this.InitializeProjectNamesListsAsync();

            if (!(this.UnassignedCategories.Contains(category)) || string.IsNullOrEmpty(inputProjectType))
            {
                return this.RedirectToPage("Error");
            }

            await this.Repository.AssignCategoryToProjectTypeAsync(category, inputProjectType);

            var assignmentObject = new
            {
#pragma warning disable IDE0037 // Use inferred member name
                category = category,
#pragma warning restore IDE0037 // Use inferred member name
                projectType = inputProjectType
            };

            return new OkObjectResult(assignmentObject);
        }

        public async Task<IActionResult> OnPostAsync([FromForm] string newCategory)
        {
            await this.InitializeProjectNamesListsAsync();
            
            if (this.AssignedCategories.Contains(newCategory) || this.UnassignedCategories.Contains(newCategory))
            {
                return this.RedirectToPage("Error");
            }

            await this.Repository.AddNewCategoryAsync(newCategory);

            return this.RedirectToPage();
        }
    }
}