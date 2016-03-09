using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarPooler.Models
{
    public class ApplicationUserViewModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Stars { get; set; }
    }
}