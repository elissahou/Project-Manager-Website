using System;
using System.ComponentModel.DataAnnotations;


namespace E9U.Tangello.Data.Entities
{
    public abstract class NamedIDBase
    {
        public int ID { get; set; }

        [Required]
        [MinLength(1)]
        public string Name { get; set; }
    }
}
