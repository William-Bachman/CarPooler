using CarPooler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarPooler.Adapters.Interfaces
{
    public interface ITripsAdapter
    {
        List<DriverJourneyViewModel> GetDriverJourneys(string username);
        List<PassengerJourneyViewModel> GetPassengerJourneys(string username);
        void EditTrip(DriverJourneyViewModel trip);
        void DeleteTrip(DriverJourneyViewModel trip);
        sbyte AddTrip(DriverJourneyViewModel trip);
        List<DriverJourneyViewModel> GetJourneys(string depCity, string destination);
        bool AddPassenger(PassengerJourneyViewModel passengerJourney);
        void CancelRequest(PassengerJourneyViewModel trip);
        void AcceptRequest(PassengerJourneyViewModel trip);
        void AddReview(ReviewViewModel review);
        UserProfileViewModel GetUserProfile(string username);
        List<ReviewViewModel> GetReviews(string username);
        void EditProfile(UserProfileViewModel profile);
        DriverJourneyViewModel GetSingleTrip(int tripId);

       
    }
}