using System;
using System.ComponentModel.DataAnnotations;


namespace E9U.Tangello.Data.Entities
{
    public abstract class NamedIDBase
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
