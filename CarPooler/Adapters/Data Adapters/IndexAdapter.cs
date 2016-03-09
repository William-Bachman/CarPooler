using CarPooler.Adapters.Interfaces;
using CarPooler.Data;
using CarPooler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarPooler.Adapters.Data_Adapters
{
    public class IndexAdapter : IIndexAdapter
    {
        //GET LIST OF JOURNEYS DEPARTING FROM THE USER'S CITY
        public List<DriverJourneyViewModel> GetDestinations(string departureCity, string destination)
        {
            List<DriverJourneyViewModel> destinations;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {

                    destinations = db.DriverJourneys.Where(x => x.DeparturePoint.Contains((departureCity == "undefined" || departureCity == "" || departureCity == null) ? x.DeparturePoint : departureCity) && x.Destination == ((destination == "undefined" || destination == "" || destination == null) ? x.Destination : destination) && x.SeatsAvailable > 0).GroupBy(x => x.Destination).OrderByDescending(x => x.Count()).Select(x => x.FirstOrDefault()).Select(x => new DriverJourneyViewModel()
                    {
                        Id = x.Id,
                        Destination = x.Destination,
                        DeparturePoint=x.DeparturePoint,
                        DestLatitud = x.DestLatitud,
                        DestLongitud = x.DestLongitud,
                        NumTripsDest = x.DestinationCounter.NumTrips,
                        NumTripsDepDest = db.DriverJourneys.Where(y => y.DeparturePoint.Contains(departureCity) && y.Destination == x.Destination).Count()
                    }).ToList();
            }            
            return destinations;
        }
    }
}