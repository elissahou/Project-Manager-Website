using System;


namespace E9U.Tangello.Data.Entities
{
    public abstract class MappingBase
    {
        public int ID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
