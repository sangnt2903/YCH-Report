using System;
using System.Collections.Generic;

namespace CalculateSalaryOfFleet.Models
{
    public partial class Trucks
    {
        public Trucks()
        {
            Drivers = new HashSet<Drivers>();
        }

        public string TruckId { get; set; }
        public string TruckType { get; set; }

        public ICollection<Drivers> Drivers { get; set; }
    }
}
