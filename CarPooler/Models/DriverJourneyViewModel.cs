using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarPooler.Models
{
    public class DriverJourneyViewModel
    {
        public int Id { get; set; }
        public string Username {get;set;}
        public string UserId {get;set;}
        public string Destination { get; set; }
        public string DestLatitud { get; set; }
        public string DestLongitud { get; set; }
        public string DeparturePoint { get; set; }
        public byte? SeatsAvailable { get; set; }
        public DateTime? Date { get; set; }
        public string ETD { get; set; }
        public string ETA { get; set; }

        public int NumTripsDest { get; set; }
        public int NumTripsDepDest { get; set; }
        public List<ApplicationUserViewModel> Users { get; set; }
        public List<PassengerJourneyViewModel> UsersConfirmed { get; set; }
        public List<PassengerJourneyViewModel> UsersNotConfirmed { get; set; }
    }
}