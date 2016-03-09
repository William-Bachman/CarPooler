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
    public class TripsApiController:ApiController
    {
        private ITripsAdapter _adapter;

        public TripsApiController()
        {
            _adapter = new TripsAdapter();
        }

        public TripsApiController(ITripsAdapter _adapter)
        {
            this._adapter = _adapter;
        }

        [Authorize]
        public IHttpActionResult Get(bool driver, string username)
        {
            if (driver) return Ok(_adapter.GetDriverJourneys(username));
            else return Ok(_adapter.GetPassengerJourneys(username));
        }

        [Authorize]
        public IHttpActionResult Post(DriverJourneyViewModel model)
        {
            sbyte respCode;
            if ((respCode=_adapter.AddTrip(model)) == -1)
                return BadRequest("All the fields except Est. time of departure and arrival are required");
            else if (respCode == -2)
                return BadRequest("Date must be entered and must be in this format: yyyy-mm-ddThh:mm:ss");
            else if (respCode == -3)
                return BadRequest("Seats available cannot be empy and must be a number");
            else
                return Ok();
            
        }

    }
}