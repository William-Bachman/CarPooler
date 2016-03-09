using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPooler.Data.Model
{
    public class Review
    {
        public int Id { get; set; }
               
        public string DriverId {get;set;}
        public string PassengerId{get;set;}
        [Required(ErrorMessage = "Please write a review")]
        public byte Stars { get; set; }
        public string Description { get; set; }
        public DateTime? ReviewDate { get; set; }

        [ForeignKey("DriverId")] //[InverseProperty("")]
        public virtual ApplicationUser Driver { get; set; }
        [ForeignKey("PassengerId")] 
        public virtual ApplicationUser Passenger { get; set; }
        
    }
}