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
using System.Security.Claims;
using System.Threading.Tasks;

namespace PurwadhikaWebApplication.APIController
{
    public class ApplicantsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Applicants
        //public IQueryable<ApplicantsModel> GetApplicantsModels()
        //{
        //    return db.ApplicantsModels;
        //}

        // GET: api/Applicants/5
        //[ResponseType(typeof(ApplicantsModel))]
        //public IHttpActionResult GetApplicantsModel(int id)
        //{
        //    ApplicantsModel applicantsModel = db.ApplicantsModels.Find(id);
        //    if (applicantsModel == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(applicantsModel);
        //}

        // PUT: api/Applicants/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutApplicantsModel(int id, ApplicantsModel applicantsModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != applicantsModel.id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(applicantsModel).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ApplicantsModelExists(id))
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

        // POST: api/Applicants
     //   [Authorize]
        [ResponseType(typeof(ApplicantsModel))]
        public async Task<IHttpActionResult> PostApplicantsModel(ApplicantsModel applicantsModel)
        {
            AuthRepository repository = new AuthRepository();
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;

            //JobMaster job = new JobMaster();

            var StudentUName = principal.Claims.Where(sid => sid.Type == "sub").FirstOrDefault();
            var studentId = await repository.FindMe(StudentUName.Value);

            //job.Id = applicantsModel.JobId;

            //job.CurrentApplicant = db.ApplicantsModels.Where(r => r.JobId == job.Id)
            //                       .Count();
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           
            applicantsModel.StudentId = studentId.Id;
            // applicantsModel.JobId = 
            //check if student has applied for that job
            if (db.ApplicantsModels.Any(r => (r.StudentId == studentId.Id && r.JobId == applicantsModel.JobId)))
            {
                throw new HttpResponseException(HttpStatusCode.Ambiguous);
            }

            db.ApplicantsModels.Add(applicantsModel);

            db.SaveChanges();
            
            

            return CreatedAtRoute("DefaultApi", new { id = applicantsModel.id, studentid = applicantsModel.StudentId}, applicantsModel);
        }

        // DELETE: api/Applicants/5
        [ResponseType(typeof(ApplicantsModel))]
        public IHttpActionResult DeleteApplicantsModel(int id)
        {
            ApplicantsModel applicantsModel = db.ApplicantsModels.Find(id);
            if (applicantsModel == null)
            {
                return NotFound();
            }

            db.ApplicantsModels.Remove(applicantsModel);
            db.SaveChanges();

            return Ok(applicantsModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ApplicantsModelExists(int id)
        {
            return db.ApplicantsModels.Count(e => e.id == id) > 0;
        }
    }
}