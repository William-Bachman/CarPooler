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
    public class UserProfileApiController:ApiController
    {
        private ITripsAdapter _adapter;

        public UserProfileApiController()
        {
            _adapter = new TripsAdapter();
        }

        public UserProfileApiController(ITripsAdapter _adapter)
        {
            this._adapter = _adapter;
        }

        [Authorize]
        public IHttpActionResult Get(string username)
        {
            if (username == null) return BadRequest("No username");
            else
            {
                UserProfileViewModel up=_adapter.GetUserProfile(username);

                if(up==null)  return Ok("Not Found");
                else return Ok(_adapter.GetUserProfile(username));              
            }
        }
        [Authorize]
        public IHttpActionResult Post(UserProfileViewModel profile)
        {
            if (profile == null)
                return BadRequest("Profile not added");

           _adapter.EditProfile(profile);
           return Ok();
           
            //    return BadRequest("Profile not added");

        }


    }
}