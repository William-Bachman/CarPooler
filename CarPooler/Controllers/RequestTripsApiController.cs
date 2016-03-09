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
    public class RequestTripsApiController:ApiController
    {
        private ITripsAdapter _adapter;

        public RequestTripsApiController()
        {
            _adapter = new TripsAdapter();
        }

        public RequestTripsApiController(ITripsAdapter _adapter)
        {
            this._adapter = _adapter;
        }

        [Authorize]
        public IHttpActionResult Post(PassengerJourneyViewModel trip)
        {
            _adapter.CancelRequest(trip);
            return Ok();
        }

        [Authorize]
        public IHttpActionResult Put(PassengerJourneyViewModel trip)
        {
            _adapter.AcceptRequest(trip);
            return Ok();
        }
    }
}