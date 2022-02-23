using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RouteAPI.Entities;

namespace RouteAPI.DataAccess
{
    public class LandmarkRepository: ILandmarkRepository
    {
        private readonly HashSet<Landmark> _landmarks;

        public LandmarkRepository()
        {
            _landmarks = new HashSet<Landmark>();
        }

        public HashSet<Landmark> GetLandmarks()
        {
            return _landmarks;
        }

        public Landmark GetLandmark(string landmark)
        {
            return _landmarks.FirstOrDefault(lm => lm.Name.Equals(landmark, StringComparison.InvariantCultureIgnoreCase));
        }

        public void SaveLandmark(Landmark mark)
        {
            _landmarks.Add(mark);
        }
    }
}
