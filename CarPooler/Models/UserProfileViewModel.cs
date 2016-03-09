using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarPooler.Models
{
    public class UserProfileViewModel
    {
        public string UserName { get; set; }
        public string PictureUrl { get; set; }
        public DateTime? DOB { get; set; }
        public string HomeTown { get; set; }
        public string Phone { get; set; }
        public List<ReviewViewModel> Reviews { get; set; }
    }
}