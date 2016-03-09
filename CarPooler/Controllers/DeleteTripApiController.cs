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
    public class DeleteTripApiController:ApiController
    {
        private ITripsAdapter _adapter;

        public DeleteTripApiController()
        {
            _adapter = new TripsAdapter();
        }

        public DeleteTripApiController(ITripsAdapter _adapter)
        {
            this._adapter = _adapter;
        }

        [Authorize]
        public IHttpActionResult Post(DriverJourneyViewModel trip)
        {
            _adapter.DeleteTrip(trip);
            return Ok();
        }
    }
    
}