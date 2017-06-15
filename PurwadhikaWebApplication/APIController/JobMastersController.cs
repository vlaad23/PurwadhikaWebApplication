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
    public class JobMastersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/JobMasters
        public IQueryable<JobMaster> GetJobMasters()
        {
            var jobList = db.JobMasters.ToList().Select(jb => new JobMaster
            {
                Id = jb.Id, JobName = jb.JobName, Image = jb.Image, AvailablePosition = jb.AvailablePosition, Description = jb.Description,
                CreatedBy = jb.CreatedBy, DateTime = jb.DateTime, CurrentApplicant = db.ApplicantsModels.Where(am => am.JobId == jb.Id).Count(), 
                ExpiredDate = jb.ExpiredDate, JobStatus = jb.JobStatus, ApprovalStatus = jb.ApprovalStatus
            }).AsQueryable();
            return jobList;
        }
        [Authorize]
        [Route("~/api/acceptedjob")]
        [HttpGet]
        public IHttpActionResult GetSortedJob()
        {
            
            var acceptedJob = db.JobMasters.ToList().Select(job => new JobMaster
            {
                Id = job.Id,
                JobName = job.JobName,
                Image = job.Image,
                AvailablePosition = job.AvailablePosition,
                Description = job.Description,
                CreatedBy = job.CreatedBy,
                DateTime = job.DateTime,
                CurrentApplicant = db.ApplicantsModels.Where(am => am.JobId == job.Id).Count(),
                JobStatus = job.JobStatus,
                ApprovalStatus = job.ApprovalStatus,
                ExpiredDate = job.ExpiredDate,
                Category = job.Category

            }).Where(job => job.ApprovalStatus == ApprovalStatus.Approved && DateTime.Now <= job.ExpiredDate && job.JobStatus == JobStatus.Open );
            return Ok(acceptedJob);
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

        //// PUT: api/JobMasters/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutJobMaster(int id, JobMaster jobMaster)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != jobMaster.Id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(jobMaster).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!JobMasterExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST: api/JobMasters
        //[ResponseType(typeof(JobMaster))]
        //public IHttpActionResult PostJobMaster(JobMaster jobMaster)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.JobMasters.Add(jobMaster);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = jobMaster.Id }, jobMaster);
        //}

        //// DELETE: api/JobMasters/5
        //[ResponseType(typeof(JobMaster))]
        //public IHttpActionResult DeleteJobMaster(int id)
        //{
        //    JobMaster jobMaster = db.JobMasters.Find(id);
        //    if (jobMaster == null)
        //    {
        //        return NotFound();
        //    }

        //    db.JobMasters.Remove(jobMaster);
        //    db.SaveChanges();

        //    return Ok(jobMaster);
        //}

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