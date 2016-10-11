﻿using System;
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
    public class JobMastersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/JobMasters
        public IQueryable<JobMaster> GetJobMasters()
        {
            return db.JobMasters;
        }

        // GET: api/JobMasters/5
        [ResponseType(typeof(JobMaster))]
        public IHttpActionResult GetJobMaster(int id)
        {
            JobMaster jobMaster = db.JobMasters.Find(id);
            if (jobMaster == null)
            {
                return NotFound();
            }

            return Ok(jobMaster);
        }

        // PUT: api/JobMasters/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutJobMaster(int id, JobMaster jobMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != jobMaster.Id)
            {
                return BadRequest();
            }

            db.Entry(jobMaster).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobMasterExists(id))
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

        // POST: api/JobMasters
        [ResponseType(typeof(JobMaster))]
        public IHttpActionResult PostJobMaster(JobMaster jobMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.JobMasters.Add(jobMaster);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = jobMaster.Id }, jobMaster);
        }

        // DELETE: api/JobMasters/5
        [ResponseType(typeof(JobMaster))]
        public IHttpActionResult DeleteJobMaster(int id)
        {
            JobMaster jobMaster = db.JobMasters.Find(id);
            if (jobMaster == null)
            {
                return NotFound();
            }

            db.JobMasters.Remove(jobMaster);
            db.SaveChanges();

            return Ok(jobMaster);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JobMasterExists(int id)
        {
            return db.JobMasters.Count(e => e.Id == id) > 0;
        }
    }
}