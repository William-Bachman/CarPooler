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
    public class ReviewDriverApiController:ApiController
    {
        private ITripsAdapter _adapter;

        public ReviewDriverApiController()
        {
            _adapter = new TripsAdapter();
        }

        public ReviewDriverApiController(ITripsAdapter _adapter)
        {
            this._adapter = _adapter;
        }

        [Authorize]
        public IHttpActionResult Post(ReviewViewModel review)
        {
            _adapter.AddReview(review);
            return Ok();
        }
    }
    
}