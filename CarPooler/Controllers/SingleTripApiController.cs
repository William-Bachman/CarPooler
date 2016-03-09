using CarPooler.Adapters.Data_Adapters;
using CarPooler.Adapters.Interfaces;
using CarPooler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;


namespace CarPooler.Controllers
{
    public class SingleTripApiController :ApiController
    {
        // GET: SingleTrip
    
        private ITripsAdapter _adapter;

        public SingleTripApiController()
        {
            _adapter = new TripsAdapter();
        }

        public SingleTripApiController(ITripsAdapter _adapter)
        {
            this._adapter = _adapter;
        }

        [Authorize]
        public IHttpActionResult Get(int tripId)
        {
            
                DriverJourneyViewModel uno =_adapter.GetSingleTrip(tripId);

                return Ok(_adapter.GetSingleTrip(tripId));              
            }
    }
}