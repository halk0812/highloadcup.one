using System;
using System.Collections.Generic;
using Common;

namespace LocationService
{
    public class LocationProvider : ILocationProvider
    {
        private List<Location> _repository = new List<Location>();
        public LocationProvider()
        {

        }

        public Location GetById(int id)
        {
            return _repository.Find(n => n.Id == id);
        }

        public void LoadLocations(List<Location> list)
        {
            _repository.AddRange(list);
        }
    }
}
