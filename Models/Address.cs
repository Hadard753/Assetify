using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assetify.Models
{
    public class Address
    {
        public int AddressID { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }
        public string Full { get; set; }
        public string Neighborhood { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool IsPublic { get; set; }
    }
}
