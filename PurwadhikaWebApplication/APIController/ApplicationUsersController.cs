using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PurwadhikaWebApplication.Models;
using PurwadhikaWebApplication.Repo;
using System.Threading.Tasks;
using System.Security.Claims;

//NOTE: Di UserViewModel perlu tambahin Email...
//CONSIDERATION: Yang mau di get nanti application user atau UserViewModel? kalo UserViewModel ya perlu email biar bisa nge-put berdasarkan email.
//              Kalo mau nampilin ApplicationUser, password kudu di hide banget
namespace PurwadhikaWebApplication.APIController
{
    public class ApplicationUsersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private AuthRepository _repo = null;


        // GET: api/ApplicationUsers
     //   [Authorize]
        public IQueryable<UserViewModel> GetApplicationUsers()
        {
            var list = db.Users.ToList().Select(e => new UserViewModel
            {
                Firstname = e.Firstname, Lastname = e.Lastname, Gender = e.Gender, Address = e.Address, Batch = e.Batch, AccountPicture = e.AccountPicture, About = e.About, Skills = e.Skills, Experience = e.Experience
            }).AsQueryable();
            return list;
        }

        // GET: api/ApplicationUsers/5
        [ResponseType(typeof(ApplicationUser))]
        public IHttpActionResult GetApplicationUser(string id)
        {
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return Ok(applicationUser);
        }

        [Authorize]
        [Route("~/api/akun/me")]
        [HttpGet]
        // GET: api/ApplicationUsers/5
        [ResponseType(typeof(ApplicationUser))]
        public async Task<IHttpActionResult> Me()
        {
            AuthRepository myRepo = new AuthRepository();
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            
            var email = principal.Claims.Where(e => e.Type == "sub").FirstOrDefault();
            var me = await myRepo.FindMe(email.Value);
            if (me == null)
            {
                return NotFound();
            }

            return Ok(me);
        }
        // PUT: api/ApplicationUsers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutApplicationUser(string email, ApplicationUser applicationUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (email != applicationUser.Email)
            {
                return BadRequest();
            }

            db.Entry(applicationUser).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationUserExists(email))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ApplicationUsers
        [ResponseType(typeof(ApplicationUser))]
      //  [AllowAnonymous]
        public async Task<IHttpActionResult> PostApplicationUser(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            try
            {
                AuthRepository auth = new AuthRepository();
                var result=await auth.RegisterUser(model);
                return Ok(result);
            }
            catch (DbUpdateException)
            {
                //if (ApplicationUserExists(applicationUser.Id))
                //{
                //    return Conflict();
                //}
                //else
                //{
                //    throw;
                //}
                throw;
            }

            //return CreatedAtRoute("DefaultApi", new { id = applicationUser.Id }, applicationUser);
        }

        // DELETE: api/ApplicationUsers/5
        [ResponseType(typeof(ApplicationUser))]
        public IHttpActionResult DeleteApplicationUser(string id)
        {
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            db.Users.Remove(applicationUser);
            db.SaveChanges();

            return Ok(applicationUser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ApplicationUserExists(string id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }
    }
}