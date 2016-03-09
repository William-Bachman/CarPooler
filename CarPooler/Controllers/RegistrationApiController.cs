using CarPooler.Data;
using CarPooler.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using CarPooler.Models;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Net.Mail;
using System.Net.Mime;


namespace CarPooler.Controllers
{
    public class RegistrationApiController : ApiController
    {
        /*
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        */
        public IHttpActionResult Post(RegisterViewModel user)
        {
            if (ModelState.IsValid)
            {
                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    if (!db.Users.Any(x => x.UserName == user.Username || x.Email == user.Email))
                    {

                            if(user.Password.Length<6) return BadRequest("The password must contain at least 6 characters");
                            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                            ApplicationUser User = new ApplicationUser()
                            {
                                UserName = user.Username,
                                Email = user.Email,
                                FirstName = user.Name,
                                LastName = user.LastName,
                            };
                            manager.Create(User,user.Password);


                            try
                            {
                                MailMessage mailMsg = new MailMessage();

                                // To
                                mailMsg.To.Add(new MailAddress("g5117983@trbvm.com", "To Name"));

                                // From
                                mailMsg.From = new MailAddress("conradasdfadsfaw@gmail.com", "From Name");

                                // Subject and multipart/alternative Body
                                mailMsg.Subject = "subject";
                                string text = "text body";
                                string html = @"<p>html body</p>";
                                mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
                                mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));

                                // Init SmtpClient and send
                                SmtpClient smtpClient = new SmtpClient("smtp.sendgrid.net", Convert.ToInt32(587));
                                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("conradasdfadsfaw@gmail.com", "asdfasdfsadf323");
                                smtpClient.Credentials = credentials;

                                smtpClient.Send(mailMsg);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }


                        /* Catch ex when testing adding index to Firstname in ApplicationUser and user inputs an existing username
                            try
                            {
                                manager.Create(model, user.Password);
                            }
                            catch (Exception e)
                            {
                                return BadRequest("FirstName exists in db");
                                Console.WriteLine(e.StackTrace);
                            }
                         * */
                    }
                    else { return BadRequest("Username or Email already exists"); }
                }

                return Ok();
            }
            else
            {
                return BadRequest("Please include all required information.");
            }
        }
    }
    public class RegisterViewModel
    {
        [Required]
        public string Name{get;set;}
        [Required]
        public string LastName{get;set;}
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
    }

}