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

namespace PurwadhikaWebApplication.APIController
{
    public class MessageMastersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/MessageMasters
        public IQueryable<MessageMaster> GetMessageMasters()
        {
            return db.MessageMasters;
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

        // POST: api/MessageMasters
        [ResponseType(typeof(MessageMaster))]
        public IHttpActionResult PostMessageMaster(MessageMaster messageMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MessageMasters.Add(messageMaster);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = messageMaster.Id }, messageMaster);
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