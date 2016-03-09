using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarPooler.Models
{
    public class ReviewViewModel
    {
        public int Id { get; set; }
        public string driverUsername { get; set; }
        public string passengerUsername { get; set; }
        public byte Stars { get; set; }
        public string Description { get; set; }
        public DateTime? ReviewDate { get; set; }
    }
}