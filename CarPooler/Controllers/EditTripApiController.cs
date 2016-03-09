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
    public class EditTripApiController:ApiController
    {

        private ITripsAdapter _adapter;

        public EditTripApiController()
        {
            _adapter = new TripsAdapter();
        }

        public EditTripApiController(ITripsAdapter _adapter)
        {
            this._adapter = _adapter;
        }

        [Authorize]
        public IHttpActionResult Post(DriverJourneyViewModel trip)
        {
            _adapter.EditTrip(trip);
            return Ok();
        }
    }

    
}