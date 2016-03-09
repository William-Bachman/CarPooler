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
    public class IndexApiController:ApiController
    {
        private IIndexAdapter _adapter;

        public IndexApiController()
        {
            _adapter = new IndexAdapter();
        }

        public IndexApiController(IIndexAdapter _adapter)
        {
            this._adapter = _adapter;
        }

        public IHttpActionResult Get(string departureCity,string destination)
        {

            List<DriverJourneyViewModel> destinations = _adapter.GetDestinations(departureCity,destination);

            if(destinations==null)
                return Ok("No destinations found");
            else
                return Ok(destinations);

        }

    }
}