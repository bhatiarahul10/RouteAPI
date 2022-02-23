using System.Collections.Generic;
using GrapgDS;
using RouteAPI.Entities;

namespace RouteAPI
{
    public interface ILandMarkManager
    {
        Landmark RegisterLandMark(string name);

        Landmark GetLandmark(string name);

        public IEnumerable<Landmark> GetLandmarks();

        public bool UpdateNeighbours(string from ,string to);

    }
}