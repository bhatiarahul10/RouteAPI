using RouteAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteAPI.DataAccess
{
    public interface ILandmarkRepository
    {
        HashSet<Landmark> GetLandmarks();

        Landmark GetLandmark(string landmark);

        public void SaveLandmark(Landmark mark);
    }
}
