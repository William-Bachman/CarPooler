using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace CarPooler.Data.Model
{
    public class PassengerJourney
    {
        public int Id { get; set; }
        public string UserId{get;set;}
        public int DriverJourneyId { get; set; }
    //    public string CityVid { get; set; }
        public bool IsConfirmed { get; set; }
        public byte NumSeats { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual DriverJourney DriverJourney { get; set; }
    }
}