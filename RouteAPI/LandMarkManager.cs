using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using GrapgDS;
using RouteAPI.DataAccess;
using RouteAPI.Entities;

namespace RouteAPI
{
    public class LandMarkManager : ILandMarkManager
    {
        private readonly ILandmarkRepository _repository;

        public LandMarkManager(ILandmarkRepository repository)
        {
            _repository = repository;
        }

        public Landmark RegisterLandMark(string name)
        {
            
            var landmark = _repository.GetLandmark(name);
            if (landmark == null)
            {
                landmark = new Landmark(name);
                _repository.SaveLandmark(landmark);

            }
            return landmark;
        }

        public Landmark GetLandmark(string name)
        {
            return _repository.GetLandmark(name);
        }

        public IEnumerable<Landmark> GetLandmarks()
        {
            return _repository.GetLandmarks();
        }

        public bool UpdateNeighbours(string from, string to)
        {
            var fromLandMark = _repository.GetLandmark(from);
            var toLandMark = _repository.GetLandmark(to);
            if (fromLandMark != null && toLandMark != null)
            {
                fromLandMark.AdjacentLandmarks.Add(toLandMark);
                return true;
            }

            return false;
        }
    }

   
}
