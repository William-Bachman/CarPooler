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
    public class SearchTripsApiController:ApiController
    {
        private ITripsAdapter _adapter;

        public SearchTripsApiController()
        {
            _adapter = new TripsAdapter();
        }

        public SearchTripsApiController(ITripsAdapter _adapter)
        {
            this._adapter = _adapter;
        }


        public IHttpActionResult Get(string depCity=null, string destination=null)
        {           
            return Ok(_adapter.GetJourneys(depCity, destination));
        }
    }
}