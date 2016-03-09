using CarPooler.Adapters.Interfaces;
using CarPooler.Data;
using CarPooler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Migrations;
using CarPooler.Data.Model;

namespace CarPooler.Adapters.Data_Adapters
{
    public class TripsAdapter : ITripsAdapter
    {

        //(AllTrips VIEW) GET LIST OF JOURNEYS TO A SPECIFIC DESTINATION DEPARTING FROM THE USER'S CITY 
        public List<DriverJourneyViewModel> GetJourneys(string depCity, string destination)
        {
            List<DriverJourneyViewModel> DriverJourneys;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {

                DriverJourneys = db.DriverJourneys.Where(x => x.SeatsAvailable > 0 && x.Destination == ((destination == "undefined" || destination == "" || destination == null) ? x.Destination : destination) && x.DeparturePoint.Contains((depCity == "undefined" || depCity == "" || depCity == null) ? x.DeparturePoint : depCity)).OrderByDescending(x => x.Date).ThenByDescending(x => x.SeatsAvailable).Select(x => new DriverJourneyViewModel()
                {
                    Id = x.Id,
                    Users = db.Users.Where(y => y.Id == x.UserId).Select(y => new ApplicationUserViewModel()
                    {
                        Username = y.UserName,
                        Stars =y.Stars
                    }).ToList(),
                    DestLatitud = x.DestLatitud,
                    DestLongitud = x.DestLongitud,
                    Destination = x.Destination,
                    DeparturePoint = x.DeparturePoint,
                    SeatsAvailable =x.SeatsAvailable,
                    Date = x.Date,
                    ETD = x.ETD,
                    ETA = x.ETA
                }).ToList();
            }
            return DriverJourneys;
        }

        //(AllTrips VIEW: Shown only for passengers and drivers who want to be passengers in another trip) ADD A PASSENGER TO A JOURNEY 
        //(WHEN USER CLICKS ON 'REQUEST SPOT')
        public bool AddPassenger(PassengerJourneyViewModel passengerJourney)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {

                if ((db.DriverJourneys.FirstOrDefault(x => x.Id == passengerJourney.TripId).SeatsAvailable) <= 0)
                    return false;
                else
                    db.DriverJourneys.FirstOrDefault(x => x.Id == passengerJourney.TripId).SeatsAvailable -= passengerJourney.NumSeats;


                if (db.PassengerJourneys.Any(x => x.User.UserName == passengerJourney.Username && x.DriverJourneyId == passengerJourney.TripId))
                {
                    PassengerJourney pj = db.PassengerJourneys.FirstOrDefault(x => x.User.UserName == passengerJourney.Username && x.DriverJourneyId == passengerJourney.TripId);
                    pj.NumSeats += passengerJourney.NumSeats;
                    pj.IsConfirmed = false;
                    db.SaveChanges();
                    return true;
                }

                db.PassengerJourneys.AddOrUpdate(x => x.Id, new PassengerJourney()
                {
                    UserId = db.Users.FirstOrDefault(x => x.UserName == passengerJourney.Username).Id,
                   
                    DriverJourneyId = passengerJourney.TripId,
                    IsConfirmed = false,
                    NumSeats = passengerJourney.NumSeats
                });

                db.SaveChanges();
                return true;
            }


        }

        //(UserTrips VIEW: Shown for passengers and drivers who are passengers in other trips) 
        //Cancel a request for a spot in a car for a passenger
        public void CancelRequest(PassengerJourneyViewModel trip)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                PassengerJourney pj = db.PassengerJourneys.FirstOrDefault(x => x.DriverJourneyId == trip.TripId && x.User.UserName == trip.Username);

                db.DriverJourneys.FirstOrDefault(x => x.Id == trip.TripId).SeatsAvailable += pj.NumSeats;

                db.PassengerJourneys.Remove(pj);
                db.SaveChanges();
            }
        }

        //(UserTrips VIEW: Shown only for drivers) Accept a request for a spot in a car from a passenger
        public void AcceptRequest(PassengerJourneyViewModel trip)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                PassengerJourney pj = db.PassengerJourneys.FirstOrDefault(x => x.DriverJourneyId == trip.TripId && x.User.UserName == trip.Username);
                pj.IsConfirmed = true;
                db.SaveChanges();
            }
        }

        //(UserTrips VIEW: Shown only for drivers) GET ALL THE JOURNEYS FOR A SPECIFIC DRIVER
        public List<DriverJourneyViewModel> GetDriverJourneys(string username)
        {
            List<DriverJourneyViewModel> DriverJourneys;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                DriverJourneys = db.DriverJourneys.Where(x => x.User.UserName == username).OrderByDescending(x=>x.Date).Select(x => new DriverJourneyViewModel()
                {
                    Id = x.Id,
                    /*
                    UsersConfirmed = db.Users.Where(y => y.PassengerJourneys.Any(z => z.Id == x.Id && z.IsConfirmed == true && z.User.UserName == y.UserName)).Select(y => new ApplicationUserViewModel()
                    {
                        
                        
                    }).ToList(),
                                       
                    */

                    UsersConfirmed = (from user in db.Users
                                      join passengerJourney in db.PassengerJourneys on user.Id equals passengerJourney.UserId
                                      where passengerJourney.IsConfirmed == true && passengerJourney.DriverJourneyId == x.Id
                                      select new PassengerJourneyViewModel()
                    {
                        Username = user.UserName,
                        NumSeats = passengerJourney.NumSeats
                    }).ToList(),                   
                    UsersNotConfirmed = (from user in db.Users join passengerJourney in db.PassengerJourneys on user.Id equals passengerJourney.UserId where passengerJourney.IsConfirmed==false && passengerJourney.DriverJourneyId == x.Id select new PassengerJourneyViewModel()
                    {
                        Username = user.UserName,
                        NumSeats = passengerJourney.NumSeats
                    }).ToList(),
                    
                    Destination = x.Destination,
                    DestLatitud = x.DestLatitud,
                    DestLongitud = x.DestLongitud,
                    DeparturePoint = x.DeparturePoint,
                    SeatsAvailable = x.SeatsAvailable,
                    Date = x.Date,
                    ETD = x.ETD,
                    ETA = x.ETA
                }).ToList();
            }
            return DriverJourneys;
        }

        //(UserTrips VIEW: Shown for passengers and drivers who are passengers in other trips) GET ALL THE JOURNEYS FOR A SPECIFIC PASSENGER
        public List<PassengerJourneyViewModel> GetPassengerJourneys(string username)
        {

            List<PassengerJourneyViewModel> PassengerJourneys;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                PassengerJourneys = db.PassengerJourneys.Where(x => x.User.UserName == username).Select(x => new PassengerJourneyViewModel()
                {
                    TripId = x.DriverJourney.Id,
                    Destination = x.DriverJourney.Destination,
                    DepPoint=x.DriverJourney.DeparturePoint,
                    DestLat = x.DriverJourney.DestLatitud,
                    DestLong = x.DriverJourney.DestLongitud,
                    CityVid = x.DriverJourney.CityVid,
                    Date = x.DriverJourney.Date,
                    Driver = x.DriverJourney.User.UserName,
                    IsConfirmed=x.IsConfirmed,
                    isDone = (x.DriverJourney.Date.CompareTo(DateTime.Now)<0),
                    NumSeats=x.NumSeats
                }).ToList();
            }
            return PassengerJourneys;
        }

        public DriverJourneyViewModel GetSingleTrip(int tripId)
        {
           DriverJourneyViewModel SingleTrip;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
               DriverJourney uno = db.DriverJourneys.FirstOrDefault(x=> x.Id == tripId);
              SingleTrip = new DriverJourneyViewModel()
                {
                    Id = uno.Id,
                    Destination = uno.Destination,
                    DeparturePoint= uno.DeparturePoint,
                    DestLatitud = uno.DestLatitud,
                    DestLongitud = uno.DestLongitud,
                    Date = uno.Date,
                  ETA=uno.ETA,
                    ETD = uno.ETD,
               };
            }
            return SingleTrip;
        }
        //(UserTrips VIEW: Shown only for drivers) EDIT A SPECIFIC JOURNEY FOR A SPECIFIC DRIVER
        public void EditTrip(DriverJourneyViewModel trip)
        {

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.DriverJourneys.AddOrUpdate(x=>x.Id,new DriverJourney(){
                Id=trip.Id,
                UserId=db.Users.FirstOrDefault(x=>x.UserName==trip.Username).Id,
                DestinationCounterId=db.DriverJourneys.FirstOrDefault(x=>x.Destination==trip.Destination).DestinationCounterId,
                Destination=trip.Destination,
                DestLatitud=trip.DestLatitud,
                DestLongitud=trip.DestLongitud,
                DeparturePoint=trip.DeparturePoint,
                SeatsAvailable=(byte)trip.SeatsAvailable,
                Date = (DateTime)trip.Date,
                ETD=trip.ETD,
                ETA=trip.ETA
                });
                db.SaveChanges();
            }
        }

        //(UserTrips VIEW: Shown only for drivers) DELETE A SPECIFIC JOURNEY FOR A SPECIFIC DRIVER
        //DriverJourneys should have a bool isDeleted, so if a driver deletes a journey, passengers can see that the trip has been canceled and not have it magically dissapear from the list of their upcoming trips. Also an email notification would be necessary.
        public void DeleteTrip(DriverJourneyViewModel trip)
        {

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                DriverJourney dj = db.DriverJourneys.FirstOrDefault(x => x.Id == trip.Id);
                dj.DestinationCounter.NumTrips -= 1;
                db.DriverJourneys.Remove(dj);
                db.SaveChanges();
            }
        }

        //(AllTrips VIEW: Shown only for all users) ADD A JOURNEY FOR A SPECIFIC DRIVER 
        public sbyte AddTrip(DriverJourneyViewModel trip)
        {
            if (trip == null || trip.Destination == "" || trip.Destination==null || trip.DeparturePoint == "" || trip.DeparturePoint==null)
                return -1;
            else if (trip.Date == null)
                return -2;
            else if (trip.SeatsAvailable == null)
                return -3;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                DestinationCounter dc=db.DestinationsCounter.FirstOrDefault(x => x.CityName == trip.Destination);
                if (dc == null)
                {
                    db.DestinationsCounter.Add(new DestinationCounter()
                    {
                        CityName = trip.Destination,
                        NumTrips = 1
                    });
                }
                else
                    dc.NumTrips += 1;

                db.SaveChanges();

                db.DriverJourneys.AddOrUpdate(x=>x.Id,new DriverJourney()
                {
                    UserId = db.Users.FirstOrDefault(y => y.UserName == trip.Username).Id,
                    DestinationCounterId=db.DestinationsCounter.FirstOrDefault(y=>y.CityName==trip.Destination).Id,
                    Destination=trip.Destination,
                    DestLatitud=trip.DestLatitud,
                    DestLongitud=trip.DestLongitud,
                    DeparturePoint=trip.DeparturePoint,
                    SeatsAvailable = (byte)trip.SeatsAvailable,
                    Date=(DateTime)trip.Date,
                    ETD=trip.ETD,
                    ETA=trip.ETA

                });
                db.SaveChanges();
            }
            return 0;
        }

        public void AddReview(ReviewViewModel review)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.DriverReviews.Add(new Review()
                {
                    DriverId = db.Users.FirstOrDefault(x => x.UserName == review.driverUsername).Id,
                    PassengerId = db.Users.FirstOrDefault(x => x.UserName == review.passengerUsername).Id,
                    Stars = review.Stars,
                    Description = review.Description,
                    ReviewDate = DateTime.Now
                });
                db.SaveChanges();
            }

        }

        public UserProfileViewModel GetUserProfile(string username)
        {
            UserProfileViewModel UserProfiles;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                
                    UserProfile up = db.UserProfiles.FirstOrDefault(x => x.User.UserName == username);
                    if (up == null)
                    {
                        return null;
                    }
                    UserProfiles = new UserProfileViewModel()
                    {
                        PictureUrl = up.PictureUrl,
                        DOB = up.DateBirth,
                        HomeTown = up.Address,
                        Phone = up.Phone,
                        Reviews = GetReviews(username)
                    };
            }
            return UserProfiles;
        }

        public void EditProfile(UserProfileViewModel profile)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.UserProfiles.AddOrUpdate(x=> x.UserId,new UserProfile(){
                UserId = db.Users.FirstOrDefault(x=> x.UserName == profile.UserName).Id,
                PictureUrl = profile.PictureUrl,
                DateBirth = profile.DOB,
                Address = profile.HomeTown,
                Phone = profile.Phone,              
            });
                db.SaveChanges();

               
            }
        }

        public List<ReviewViewModel> GetReviews(string username)
        {
            List<ReviewViewModel> lr;
            using (ApplicationDbContext db = new ApplicationDbContext())            
            {
                lr=db.DriverReviews.Where(x=>x.Driver.UserName==username).Select(x => new ReviewViewModel()
                {
                    passengerUsername=x.Passenger.UserName,
                    Stars=x.Stars,
                    Description=x.Description,
                    ReviewDate=x.ReviewDate
                }).ToList();
                
            }
            return lr;
        }
       
    }
}