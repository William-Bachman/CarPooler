using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPooler.Data.Model
{
    public class UserProfile
    {
        [Key,ForeignKey("User")]
        public string UserId{get;set;}
        public string PictureUrl { get; set; }
        public DateTime? DateBirth { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}