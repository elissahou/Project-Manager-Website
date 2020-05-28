using System;
using System.ComponentModel.DataAnnotations;


namespace E9U.Tangello.Data.Entities
{
    public class CategoryToProjectTypeMapping : MappingBase
    {
        [Required]
        public int CategoryID { get; set; }

        [Required]
        public int ProjectTypeID { get; set; }
    }
}
