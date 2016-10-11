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
    public class AnnouncementMastersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/AnnouncementMasters
        public IQueryable<AnnouncementMaster> GetAnnouncementMasters()
        {
            return db.AnnouncementMasters;
        }

        // GET: api/AnnouncementMasters/5
        [ResponseType(typeof(AnnouncementMaster))]
        public IHttpActionResult GetAnnouncementMaster(int id)
        {
            AnnouncementMaster announcementMaster = db.AnnouncementMasters.Find(id);
            if (announcementMaster == null)
            {
                return NotFound();
            }

            return Ok(announcementMaster);
        }

        // PUT: api/AnnouncementMasters/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAnnouncementMaster(int id, AnnouncementMaster announcementMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != announcementMaster.Id)
            {
                return BadRequest();
            }

            db.Entry(announcementMaster).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnnouncementMasterExists(id))
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

        // POST: api/AnnouncementMasters
        [ResponseType(typeof(AnnouncementMaster))]
        public IHttpActionResult PostAnnouncementMaster(AnnouncementMaster announcementMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AnnouncementMasters.Add(announcementMaster);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = announcementMaster.Id }, announcementMaster);
        }

        // DELETE: api/AnnouncementMasters/5
        [ResponseType(typeof(AnnouncementMaster))]
        public IHttpActionResult DeleteAnnouncementMaster(int id)
        {
            AnnouncementMaster announcementMaster = db.AnnouncementMasters.Find(id);
            if (announcementMaster == null)
            {
                return NotFound();
            }

            db.AnnouncementMasters.Remove(announcementMaster);
            db.SaveChanges();

            return Ok(announcementMaster);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AnnouncementMasterExists(int id)
        {
            return db.AnnouncementMasters.Count(e => e.Id == id) > 0;
        }
    }
}