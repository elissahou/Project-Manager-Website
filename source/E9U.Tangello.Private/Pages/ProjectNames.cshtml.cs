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


        //public ProjectNamesModel()
        //{
        //    this.ProjectNames = new List<string>()
        //    {
        //        "Tangerine",
        //        "Lemon",
        //        "Pamplemousse"
        //    };

        //    this.ProjectNames.Sort();
        //}

        public void OnGet([FromQuery] string categoryName)
        {
            this.ProjectNames = new List<string>()
            {
                "Tangerine",
                "Lemon",
                "Pamplemousse"
            };

            this.ProjectNames.Sort();

            this.CategoryName = categoryName;
        }

        public IActionResult OnPost([FromForm] string newProjectName, [FromQuery] string categoryName)
        {
            this.CategoryName = categoryName;

            Console.WriteLine(newProjectName);

            return this.RedirectToPage();
        }
    }
}