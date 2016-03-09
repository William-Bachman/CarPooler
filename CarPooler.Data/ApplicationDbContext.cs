using CarPooler.Data.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CarPooler.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<DriverJourney> DriverJourneys { get; set; }
        public DbSet<PassengerJourney> PassengerJourneys { get; set; }
        public DbSet<Review> DriverReviews { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<DestinationCounter> DestinationsCounter { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

    }
}