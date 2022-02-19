using System;
using GrapgDS;
using RouteAPI.Entities;

namespace RouteAPI
{
    public class LandMarkManager : ILandMarkManager
    {
        public Landmark RegisterLandMark(string name)
        {
            return new Landmark(name);
        }

        public Landmark RemoveALandMark(string name)
        {
            throw new NotImplementedException();
        }
    }
}
