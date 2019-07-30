using Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LocationService
{
    public interface ILocationProvider
    {
        void LoadLocations(List<Location> list);
        Task<Location> GetByIdAsync(UInt32 id);
        Task<UInt32[]> GetIdByCountryAndDistanceAsync(string country, int? toDistance);
    }
}
