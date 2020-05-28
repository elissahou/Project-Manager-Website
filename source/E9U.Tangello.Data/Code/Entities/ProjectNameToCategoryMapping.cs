using System;
using System.ComponentModel.DataAnnotations;


namespace E9U.Tangello.Data.Entities
{
    public class ProjectNameToCategoryMapping : MappingBase
    {
        [Required]
        public int ProjectNameID { get; set; }

        [Required]
        public int CategoryID { get; set; }
    }
}
