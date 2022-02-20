using System;

namespace RouteAPI.Entities
{
    public class Landmark
    {
        public int Index { get; }

        public string Name { get; }

        public Landmark(string name)
        {
            Name = name;
        }

        public override bool Equals(object? obj) => ((Landmark)obj).Name.Equals(Name, StringComparison.InvariantCultureIgnoreCase);

        protected bool Equals(Landmark other)
        {
            return Name == other.Name;
        }

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }
    }
}
