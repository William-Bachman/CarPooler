using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarPooler.Models
{
    public class PassengerJourneyViewModel
    {
        public int TripId { get; set; }
        public string Username{get;set;}
        public string Destination { get; set; }
        public string CityVid { get; set; }
        public DateTime? Date { get; set; }
        public string Driver { get; set; }
        public bool IsConfirmed { get; set; }
        public bool isDone { get; set; }
        public byte NumSeats { get; set; }
        public string DestLat { get; set; }
        public string DestLong { get; set; }
        public string DepPoint { get; set; }
    }
}