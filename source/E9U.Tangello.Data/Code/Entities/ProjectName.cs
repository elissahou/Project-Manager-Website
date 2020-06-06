using System;


namespace E9U.Tangello.Data.Entities
{
    public class ProjectName : NamedIDBase, IEquatable<ProjectName>
    {
        public bool Equals(ProjectName other)
        {
            var equals = this.Name == other.Name;
            return equals;
        }
    }
}
