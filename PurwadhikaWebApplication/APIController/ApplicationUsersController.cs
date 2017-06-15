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
using System.Web;
using System.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using PurwadhikaWebApplication.Providers;
using System;

//NOTE: Di UserViewModel perlu tambahin Email...
//CONSIDERATION: Yang mau di get nanti application user atau UserViewModel? kalo UserViewModel ya perlu email biar bisa nge-put berdasarkan email.
//              Kalo mau nampilin ApplicationUser, password kudu di hide banget
namespace PurwadhikaWebApplication.APIController
{
    public class ApplicationUsersController : ApiController
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        // private AuthRepository _repo = null;
        private const string Container = "avatar";

        // GET: api/ApplicationUsers

        [Authorize] //get all user data
        public async Task<IHttpActionResult> GetApplicationUsers()
        {
            //so far function ini belom kepake, jaga jaga aja just in case
            AuthRepository myRepo = new AuthRepository();
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;

            var findMe = principal.Claims.Where(e => e.Type == "sub").FirstOrDefault();
            var me = await myRepo.FindMe(findMe.Value);

            var list = db.Users.ToList().Select(e => new ApplicationUser
            {
                UserName = e.UserName,
                Firstname = e.Firstname,
                Lastname = e.Lastname,
                Gender = e.Gender,
                Address = e.Address,
                Batch = e.Batch,
                AccountPicture = e.AccountPicture,
                About = e.About,
                Skills = e.Skills,
                Experience = e.Experience,
                InstanceName = e.InstanceName
               
            }).Where(usr => usr.UserName != me.UserName).AsQueryable();
            return Ok(list);
        }

        [Authorize] //get user for message input
        [Route("~/api/getuserlist")]
        [HttpGet]
        public async Task<IHttpActionResult> FilterApplicationUser()
        {
            AuthRepository myRepo = new AuthRepository();
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;

            var findMe = principal.Claims.Where(e => e.Type == "sub").FirstOrDefault();
            var me = await myRepo.FindMe(findMe.Value);

            var userList = db.Users.ToList().Select(usr => new ApplicationUser
            {
                UserName = usr.UserName,
                Firstname = usr.Firstname,
                Lastname = usr.Lastname,
                InstanceName = usr.InstanceName
            }).Where(fusr => fusr.UserName != me.UserName)
            .OrderBy(name => (name.Firstname != null)? name.Firstname : name.InstanceName) //order by alphabetical order done!
            .AsQueryable();
            return Ok(userList);
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

        [Authorize]
        [Route("~/api/akun/upload")]
        [HttpPost]
        public async Task<IHttpActionResult> UploadPicture()
        {

            //1. Define mekanisme azure storage
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var accountName = ConfigurationManager.AppSettings["storage:account:name"];
            var accountKey = ConfigurationManager.AppSettings["storage:account:key"];
            var storageAccount = new CloudStorageAccount(new StorageCredentials(accountName, accountKey), true);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer imagesContainer = blobClient.GetContainerReference(Container);
            var provider = new AzureStorageMultipartFormDataStreamProvider(imagesContainer);

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error has occured: {ex.Message}");
            }
            //2. Define File name
            var filename = provider.FileData.FirstOrDefault()?.LocalFileName;
            if (string.IsNullOrEmpty(filename))
            {
                return BadRequest("An error has occured. Please try again.");
            }
            //3. Method find user by Auth Token
            AuthRepository repo = new AuthRepository();
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;

            var username = principal.Claims.Where(e => e.Type == "sub").FirstOrDefault();
            var profile = await repo.FindUser(username.Value);

            if (profile == null)
            {
                return NotFound();
            }
            //Set Account Picture to Uri
            profile.AccountPicture = provider._blobContainer.Uri.AbsoluteUri + "/" + filename;
            var update = await repo.EditMe(profile);
            return Ok(update);
        }
        
        [Authorize]
        [Route("~/api/akun/changepic")]
        [HttpPut]
        public async Task<IHttpActionResult> ChangePic(EditViewModel newPic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AuthRepository profileRepo = new AuthRepository();
            ClaimsPrincipal myPrincipal = Request.GetRequestContext().Principal as ClaimsPrincipal;

            var getUName = myPrincipal.Claims.Where(e => e.Type == "sub").FirstOrDefault();
            var myProfile = await profileRepo.FindUser(getUName.Value);

            if (myProfile == null)
            {
                return NotFound();
            }

            myProfile.AccountPicture = newPic.uri;

            var updated = await profileRepo.EditMe(myProfile);

            return Ok(updated);
        }

        [Authorize]
        [Route("~/api/akun/me")]
        [HttpPut]
        public async Task<IHttpActionResult> EditMe(EditViewModel updateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            AuthRepository profileRepo = new AuthRepository();
            ClaimsPrincipal myPrincipal = Request.GetRequestContext().Principal as ClaimsPrincipal;

            var getUName = myPrincipal.Claims.Where(e => e.Type == "sub").FirstOrDefault();
            var myProfile = await profileRepo.FindUser(getUName.Value);

            if (myProfile == null)
            {
                return NotFound();
            }

            myProfile.About = updateModel.About;
            myProfile.Address = updateModel.Address;
            myProfile.Experience = updateModel.Experience;
            myProfile.Skills = updateModel.Skills;
            myProfile.PhoneNumber = updateModel.PhoneNumber;


            if (!string.IsNullOrEmpty(updateModel.Password))
            {
                var newPassword = profileRepo.HashPassword(updateModel.Password);
                myProfile.PasswordHash = newPassword;
            }

            var update = await profileRepo.EditMe(myProfile);

            return Ok(update);
        }
        //[HttpPost]
        //[ActionName("Edit")]
        //public async Task<IHttpActionResult> EditProfile(EditViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await Appli
        //    }
        //}
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
                var result = await auth.RegisterUser(model);
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