using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class Location
    {
        public UInt32 Id { get; set; }
        public string Place { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public UInt32 Distance { get; set; }
    }
}
