using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace LocationService
{
    public class LocationProvider : ILocationProvider
    {
        private List<Location> _repository = new List<Location>();
        public LocationProvider()
        {

        }

        public Location GetById(uint id)
        {
            return _repository.Find(n => n.Id == id);
        }

        public uint[] GetIdByCountryAndDistance(string country, int? toDistance)
        {
            IQueryable<Location> query = _repository.AsQueryable();
            if(!string.IsNullOrEmpty(country))
            {
                query = query.Where(n => n.Country.ToLower() == country.ToLower());
            }
            if(toDistance!=null)
            {
                query = query.Where(n => n.Distance < toDistance);
            }
            return query.Select(m => m.Id).ToArray();
        }

        public void LoadLocations(List<Location> list)
        {
            _repository.AddRange(list);
        }
    }
}
