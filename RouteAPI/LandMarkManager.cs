using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using GrapgDS;
using RouteAPI.Entities;

namespace RouteAPI
{
    public class LandMarkManager : ILandMarkManager
    {
        public IList<Landmark> AllLandmarks { get; }

        public LandMarkManager()
        {
            AllLandmarks = new List<Landmark>();
        }

        public Landmark RegisterLandMark(string name)
        {
            var landmark =
                AllLandmarks.FirstOrDefault(lm => lm.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
            if (landmark == null)
            {
                landmark = new Landmark(name);
                AllLandmarks.Add(landmark);
            }
            return landmark;
        }

        public Landmark GetLandmark(string name)
        {
            return AllLandmarks.FirstOrDefault(lm => lm.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }


        public Landmark RemoveALandMark(string name)
        {
            throw new NotImplementedException();
        }
    }
}
