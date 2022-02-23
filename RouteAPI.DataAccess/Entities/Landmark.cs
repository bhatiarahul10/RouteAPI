using System;
using System.Collections.Generic;

namespace RouteAPI.Entities
{
    public class Landmark
    {
        public List<Landmark> AdjacentLandmarks { get; }

        public string Name { get; }

        public Landmark(string name)
        {
            Name = name;
            AdjacentLandmarks = new List<Landmark>();
        }

        public override bool Equals(object? obj) => ((Landmark)obj).Name.Equals(Name, StringComparison.InvariantCultureIgnoreCase);

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }
    }
}
