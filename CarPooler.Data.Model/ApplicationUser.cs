using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace CarPooler.Data.Model
{
    public class ApplicationUser : IdentityUser
    {
        //[MaxLength(20)] //Required for Indexing
        //[Index("IndexFirstName",IsUnique=true)]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Stars { get; set; }
        //public virtual UserProfile UserProfile{get;set;}
        public virtual List<Review> Reviews { get; set; }
        //public virtual List<DriverJourney> DriverJourneys { get; set; }
        //public virtual List<PassengerJourney> PassengerJourneys { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }
}