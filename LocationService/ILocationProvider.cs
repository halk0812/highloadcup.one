using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocationService
{
    public interface ILocationProvider
    {
        void LoadLocations(List<Location> list);
        Location GetById(int id);
    }
}
