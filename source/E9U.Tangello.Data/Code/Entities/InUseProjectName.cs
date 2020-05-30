using System;
using System.ComponentModel.DataAnnotations;


namespace E9U.Tangello.Data.Entities
{
    class InUseProjectName
    {
        public int ID { get; set; }

        [Required]
        public int ProjectNameID { get; set; }
        public ProjectName ProjectName { get; set; }

        [MaxLength(2000)]
        public string ProjectDescription { get; set; }
    }
}