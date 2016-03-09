using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarPooler.Data.Model
{
    public class DestinationCounter
    {
        public int Id { get; set; }
        public string CityName { get; set; }
        public int NumTrips { get; set; }
        public virtual List<DriverJourney> DriverJourneys { get; set; }
    }
}