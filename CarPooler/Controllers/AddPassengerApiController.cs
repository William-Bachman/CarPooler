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
    public class AddPassengerApiController : ApiController
    {

        private ITripsAdapter _adapter;

        public AddPassengerApiController()
        {
            _adapter = new TripsAdapter();
        }

        public AddPassengerApiController(ITripsAdapter _adapter)
        {
            this._adapter = _adapter;
        }

        [Authorize]
        public IHttpActionResult Post(PassengerJourneyViewModel passengerJourney)
        {
            if (_adapter.AddPassenger(passengerJourney))
                return Ok();
            else
                return BadRequest("There are no more spots available");

        }
    }


}