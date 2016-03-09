using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPooler.Data.Model
{
    public class DriverJourney
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int DestinationCounterId { get; set; }
        public string CityVid { get; set; }
        [Required(ErrorMessage="Please enter your destination")]
        public string Destination { get; set; }
        public string DestLatitud { get; set; }
        public string DestLongitud { get; set; }
        [Required(ErrorMessage = "Please enter your departure point")]
        public string DeparturePoint { get; set; }
        [Required(ErrorMessage = "Please enter seats available")]
        public byte SeatsAvailable { get; set; }
        [Required(ErrorMessage="Please enter your departure date")]
        public DateTime Date { get; set; }
        [Display(Name="Leave it blank if you still don't know it")]
        public string ETD {get;set;}
        [Display(Name="Leave it blank if you still don't know it")]
        public string ETA { get; set; }
        public virtual ApplicationUser User { get; set; }        
        public virtual DestinationCounter DestinationCounter { get; set; }
        //public virtual List<PassengerJourney> PassengerJourneys { get; set; }
    }
}