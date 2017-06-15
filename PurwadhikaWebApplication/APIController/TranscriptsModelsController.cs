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
using System.Web.Http.Controllers;

namespace PurwadhikaWebApplication.APIController
{
    public class TranscriptsModelsController : ApiController
    {
       
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/TranscriptsModels
        public IQueryable<TranscriptsModel> GetTranscriptsModels()
        {
            return db.TranscriptsModels;
        }

        //GET My Transcript
        [Authorize]
        [Route("~/api/myTranscript")]
        [HttpGet]
        [ResponseType(typeof(ApplicationUser))]
        public async Task<IHttpActionResult> GetMyTranscript()
        {

            AuthRepository Repository = new AuthRepository();
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;

            var studentUserName = principal.Claims.Where(sid => sid.Type == "sub").FirstOrDefault();
            var studentId = await Repository.FindMe(studentUserName.Value);

            var transcriptList = db.TranscriptsModels.ToList()
                .Select(t => new TranscriptsModel { StudentId = t.StudentId, FileName = t.FileName, FileUrl = t.FileUrl })
                .Where(t => t.StudentId == studentId.Id);

            return Ok(transcriptList);
        }
        // GET: api/TranscriptsModels/5
        [ResponseType(typeof(TranscriptsModel))]
        public IHttpActionResult GetTranscriptsModel(int id)
        {
            TranscriptsModel transcriptsModel = db.TranscriptsModels.Find(id);
            if (transcriptsModel == null)
            {
                return NotFound();
            }

            return Ok(transcriptsModel);
        }

        // PUT: api/TranscriptsModels/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTranscriptsModel(int id, TranscriptsModel transcriptsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != transcriptsModel.id)
            {
                return BadRequest();
            }

            db.Entry(transcriptsModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TranscriptsModelExists(id))
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

        // POST: api/TranscriptsModels
        [Authorize]
        [ResponseType(typeof(TranscriptsModel))]
        public IHttpActionResult PostTranscriptsModel(TranscriptsModel transcriptsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TranscriptsModels.Add(transcriptsModel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = transcriptsModel.id }, transcriptsModel);
        }

        // DELETE: api/TranscriptsModels/5
        [ResponseType(typeof(TranscriptsModel))]
        public IHttpActionResult DeleteTranscriptsModel(int id)
        {
            TranscriptsModel transcriptsModel = db.TranscriptsModels.Find(id);
            if (transcriptsModel == null)
            {
                return NotFound();
            }

            db.TranscriptsModels.Remove(transcriptsModel);
            db.SaveChanges();

            return Ok(transcriptsModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TranscriptsModelExists(int id)
        {
            return db.TranscriptsModels.Count(e => e.id == id) > 0;
        }
    }
}