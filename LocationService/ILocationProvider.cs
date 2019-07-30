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
        Location GetById(UInt32 id);
        UInt32[] GetIdByCountryAndDistance(string country, int? toDistance);
    }
}
