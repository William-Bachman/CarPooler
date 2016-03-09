using CarPooler.Data.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Migrations;

namespace CarPooler.Data
{
    public class Seeder
    {
         string Id1;
         string Id2;
         string Id3;

        public void Seed(ApplicationDbContext db, bool users = true, bool roles = true, bool cars = true, bool profiles = true, bool passengerJourneys = true, bool reviews = true, bool driverJourneys = true, bool destinationsCounter = true)
        {

            if (roles) SeedRoles(db);
            if (users) SeedUsers(db);
            if (profiles) SeedUserProfiles(db);
            if (reviews) SeedReviews(db);
            if (destinationsCounter) SeedDestinationsCounter(db);
            if (driverJourneys) SeedDriverJourneys(db);
            if (passengerJourneys) SeedPassengerJourneys(db);
            
            

        }

        private  void SeedRoles(ApplicationDbContext db)
        {
            var manager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            if (!manager.RoleExists("User")) manager.Create<IdentityRole, String>(new IdentityRole() { Name = "User" });
            if (!manager.RoleExists("Admin")) manager.Create(new IdentityRole() { Name = "Admin" });
        }

        private  void SeedUsers(ApplicationDbContext db)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            ApplicationUser user1 = new ApplicationUser()
            {
                UserName = "Doug",
                Email = "Doug@CarPooler.com",
                FirstName = "Doug",
                LastName = "Smith",
                Stars=5

            };

            ApplicationUser user2 = new ApplicationUser()
            {
                UserName = "Martha",
                Email = "Martha@CarPooler.com",
                FirstName = "Martha",
                LastName = "Jones",
                Stars = 5
            };

            ApplicationUser user3 = new ApplicationUser()
            {
                UserName = "Thomas",
                Email = "Thomas@CarPooler.com",
                FirstName = "Thomas",
                LastName = "Brown",
                Stars = 0
            };

            if (!db.Users.Any(x => x.UserName == "Doug")) { manager.Create(user1, "123123"); manager.AddToRole(user1.Id, "User"); }
            if (!db.Users.Any(x => x.UserName == "Martha")) { manager.Create(user2, "123123"); manager.AddToRole(user2.Id, "User"); }
            if (!db.Users.Any(x => x.UserName == "Thomas")) { manager.Create(user3, "123123"); manager.AddToRole(user3.Id, "User"); }


            Id1 = db.Users.FirstOrDefault(x => x.UserName == "Doug").Id; //Why does user1.Id throw an error?
            Id2 = db.Users.FirstOrDefault(x => x.UserName == "Martha").Id;
            Id3 = db.Users.FirstOrDefault(x => x.UserName == "Thomas").Id;

        }

        private void SeedUserProfiles(ApplicationDbContext db)
        {
            db.UserProfiles.AddOrUpdate(x => x.UserId,
                new UserProfile() { UserId = Id1, PictureUrl = "http://3.bp.blogspot.com/-MDjtcBXV86w/UPISWnH014I/AAAAAAABTx4/Y0LgB2EkHXo/s640/021811-portrait-stephen-curry.jpg", DateBirth = DateTime.Now, Address = "Oakland, Ca", Phone = "777-7777" },
                new UserProfile() { UserId = Id2, PictureUrl = "http://images1.fanpop.com/images/photos/1300000/Tila-tila-tequila-1310682-1024-768.jpg", DateBirth = DateTime.Now, Address = "San Diego, Ca", Phone = "444-4444" },
                new UserProfile() { UserId = Id3, PictureUrl = "http://img3.rnkr-static.com/list_img/4449/1984449/full/most-underrated-frank-sinatra-songs.jpg", DateBirth = DateTime.Now, Address = "Las Vegas, NV", Phone = "888-8888" }
                );
        }

        private  void SeedReviews(ApplicationDbContext db)
        {
            db.DriverReviews.AddOrUpdate(x => x.Id,
                new Review() { Id = 1, DriverId = Id1, PassengerId = Id3, Description = "Doug is a great Driver...", Stars=5, ReviewDate = null },
                new Review() { Id = 2, DriverId = Id2, PassengerId = Id3, Description = "I had a great journey with Martha...", Stars = 5, ReviewDate = null }

            );
        }

        private void SeedDestinationsCounter(ApplicationDbContext db)
        {
            db.DestinationsCounter.AddOrUpdate(x => x.Id,
                 new DestinationCounter() { Id = 1, CityName = "Los Angeles", NumTrips = 1 },
                 new DestinationCounter() { Id = 2, CityName = "Las Vegas", NumTrips = 2 },
                 new DestinationCounter() {Id = 3, CityName = "San Diego", NumTrips = 1},
                 new DestinationCounter() {Id = 4, CityName = "Santa Barbara", NumTrips = 1},
                 new DestinationCounter() {Id = 5, CityName = "San Francisco", NumTrips = 1}
            );
        }

        private  void SeedDriverJourneys(ApplicationDbContext db)
        {
            db.DriverJourneys.AddOrUpdate(x => x.Id,
                 new DriverJourney() { Id = 1, UserId = Id1, DestinationCounterId= 1, CityVid = "https://www.youtube.com/embed/bTvr_2v-0HI", Destination = "Los Angeles", DestLatitud = "34.052234", DestLongitud = "-118.243685", DeparturePoint = "Grand Ave, Oakland, CA, USA", SeatsAvailable = 3, Date = DateTime.Now.AddDays(12), ETD = null, ETA = null },
                 new DriverJourney() { Id = 2, UserId = Id1, DestinationCounterId = 2, CityVid = "https://www.youtube.com/embed/gasI6cyjkvM", Destination = "Las Vegas", DestLatitud = "36.169941", DestLongitud = "-115.139830", DeparturePoint = "Grand Ave, Oakland, CA, USA", SeatsAvailable = 3, Date = DateTime.Now.AddDays(7), ETD = null, ETA = null },
                 new DriverJourney() { Id = 3, UserId = Id1, DestinationCounterId = 3, CityVid = "https://www.youtube.com/embed/-KMZW_zwRfc", Destination = "San Diego", DestLatitud = "32.8245525", DestLongitud = "-117.0951632", DeparturePoint = "Grand Ave, Oakland, CA, USA", SeatsAvailable = 1, Date = DateTime.Now.AddDays(2), ETD = null, ETA = null },
                 new DriverJourney() { Id = 4, UserId = Id1, DestinationCounterId =4, CityVid = "https://www.youtube.com/embed/Dd1Qy3BqWjM", Destination = "Santa Barbara", DestLatitud = "34.4281937", DestLongitud = "-119.702067", DeparturePoint = "Grand Ave, Oakland, CA, USA", SeatsAvailable = 4, Date = DateTime.Now.AddDays(3), ETD = null, ETA = null },
                 new DriverJourney() { Id = 5, UserId = Id1, DestinationCounterId = 5, CityVid = "https://www.youtube.com/embed/Oo6iAxf4si0", Destination = "San Francisco", DestLatitud = "37.7761763", DestLongitud = "-122.3703333", DeparturePoint = "Grand Ave, Oakland, CA, USA", SeatsAvailable = 4, Date = DateTime.Now.AddDays(5), ETD = null, ETA = null },
                 new DriverJourney() { Id = 6, UserId = Id1, DestinationCounterId = 2, CityVid = "https://www.youtube.com/embed/gasI6cyjkvM", Destination = "Las Vegas", DestLatitud = "36.169941", DestLongitud = "-115.139830", DeparturePoint = "Grand Ave, Oakland, CA, USA", SeatsAvailable = 3, Date = DateTime.Now.AddDays(1), ETD = null, ETA = null }
        );

        }

        private  void SeedPassengerJourneys(ApplicationDbContext db)
        {
            db.PassengerJourneys.AddOrUpdate(x => x.Id,
                 new PassengerJourney() { Id = 1, UserId = Id3, DriverJourneyId = 1, IsConfirmed = false, NumSeats=1},
                 new PassengerJourney() { Id = 2, UserId = Id3, DriverJourneyId = 2, IsConfirmed = true, NumSeats = 2 },
                 new PassengerJourney() { Id = 3, UserId = Id2, DriverJourneyId = 1, IsConfirmed = true, NumSeats = 2 }
                 );
        }
       

    }
}