using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PurwadhikaWebApplication.Models;
using System.Threading.Tasks;
using PurwadhikaWebApplication.Repo;
using System.Security.Claims;

namespace PurwadhikaWebApplication.APIController
{
    public class MessageMastersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        //private AuthRepository _repo = null;

        // GET: api/MessageMasters
        public IQueryable<MessageMaster> GetMessageMasters()
        {
            return db.MessageMasters;
        }
        
        //INBOX
        [Authorize]
        [Route("~/api/mymessage/inbox")]
        [HttpGet]

        [ResponseType(typeof(ApplicationUser))]
        public async Task<IHttpActionResult> MyInbox()
        {
            AuthRepository repository = new AuthRepository();
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;

            var myId = principal.Claims.Where(e => e.Type == "sub").FirstOrDefault();
            //var myself = await repository.FindMe(myId.Value);
               
            var myBox = db.MessageMasters.ToList()
                .Select(e => new MessageViewModel { From = e.From, To = e.To, Subject = e.Subject, Message = e.Message, DateTime = e.DateTime })
                .Where(e => e.To == myId.Value);

            return Ok(myBox);
        }

        //SENT
        [Authorize]
        [Route("~/api/mymessage/sent")]
        [HttpGet]

        [ResponseType(typeof(ApplicationUser))]
        public async Task<IHttpActionResult> MySentBox()
        {
            AuthRepository repository = new AuthRepository();
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;

            var myId = principal.Claims.Where(e => e.Type == "sub").FirstOrDefault();
            //var myself = await repository.FindMe(myId.Value);

            var mySentBox = db.MessageMasters.ToList()
                .Select(e => new MessageViewModel { From = e.From, To = e.To, Subject = e.Subject, Message = e.Message, DateTime = e.DateTime })
                .Where(e => e.From == myId.Value);

            return Ok(mySentBox);
        }

        // GET: api/MessageMasters/5
        [ResponseType(typeof(MessageMaster))]
        public IHttpActionResult GetMessageMaster(int id)
        {
            MessageMaster messageMaster = db.MessageMasters.Find(id);
            if (messageMaster == null)
            {
                return NotFound();
            }

            return Ok(messageMaster);
        }

        // PUT: api/MessageMasters/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMessageMaster(int id, MessageMaster messageMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != messageMaster.Id)
            {
                return BadRequest();
            }

            db.Entry(messageMaster).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageMasterExists(id))
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

        [Authorize]
        [Route("~/api/mymessage/compose", Name ="ComposeMessage")]
        [HttpPost]
        // POST: api/MessageMasters
        [ResponseType(typeof(MessageMaster))]
        public IHttpActionResult PostMessageMaster(MessageMaster messageMaster)
        {
            AuthRepository repository = new AuthRepository();
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;

            var myId = principal.Claims.Where(e => e.Type == "sub").FirstOrDefault();
           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            messageMaster.From = myId.Value;

            db.MessageMasters.Add(messageMaster);
            db.SaveChanges();

            return CreatedAtRoute("ComposeMessage", new { id = messageMaster.Id, from = messageMaster.From}, messageMaster);
        }

        // DELETE: api/MessageMasters/5
        [ResponseType(typeof(MessageMaster))]
        public IHttpActionResult DeleteMessageMaster(int id)
        {
            MessageMaster messageMaster = db.MessageMasters.Find(id);
            if (messageMaster == null)
            {
                return NotFound();
            }

            db.MessageMasters.Remove(messageMaster);
            db.SaveChanges();

            return Ok(messageMaster);
        }
        [Authorize]
        [Route("~/api/mymessage/scheduler")]
        [HttpPut]
        public IHttpActionResult InputScheduler(MessageMaster msg)
        {
            var msgMaster = db.MessageMasters.ToList();
            foreach (var message in msgMaster)
            {
                message.DeleteIn = msg.DeleteIn;
                message.ExpiredIn = message.DateTime.AddDays(message.DeleteIn); //<-----Deploy With This
                //message.ExpiredIn = message.DateTime.AddSeconds(message.DeleteIn); //Test Purpose Only

                db.Entry(message).State = EntityState.Modified;
            }

            db.SaveChanges();
            //messageMaster.ExpiredIn = messageMaster.DateTime.AddDays(messageMaster.DeleteIn);

            //db.Entry(messageMaster).State = EntityState.Modified;

            return Ok(new HttpResponseException(HttpStatusCode.OK));
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MessageMasterExists(int id)
        {
            return db.MessageMasters.Count(e => e.Id == id) > 0;
        }
    }
}