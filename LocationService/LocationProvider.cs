using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;

namespace LocationService
{
    public class LocationProvider : ILocationProvider
    {
        private List<Location> _repository = new List<Location>();
        public LocationProvider()
        {

        }

        public async Task<Location> GetByIdAsync(uint id)
        {
            return await Task.Run(()=>_repository.Find(n => n.Id == id));
        }

        public async Task<uint[]> GetIdByCountryAndDistanceAsync(string country, int? toDistance)
        {
            return await Task.Run(() =>
            {
                if (string.IsNullOrEmpty(country) && toDistance == null)
                    return new uint[0];
                IQueryable<Location> query = _repository.AsQueryable();
                if (!string.IsNullOrEmpty(country))
                {
                    query = query.Where(n => n.Country.ToLower() == country.ToLower());
                }
                if (toDistance != null)
                {
                    query = query.Where(n => n.Distance < toDistance);
                }
                return query.Select(m => m.Id).ToArray();
            });
        }

        public void LoadLocations(List<Location> list)
        {
            _repository.AddRange(list);
        }
    }
}
