using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PurwadhikaWebApplication.Models;
using Microsoft.AspNet.Identity;
using Microsoft.WindowsAzure.Storage.Blob;

namespace PurwadhikaWebApplication.Controllers
{
    public class JobController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public BlobStorageServices _blobStorageService = new BlobStorageServices();

        public ActionResult IndexMaster()
        {
            return View();
        }
        public PartialViewResult JobIndexContent()
        {

            var currentUserName = User.Identity.GetUserName();
            
            var model = db.JobMasters;



            var y = model.Where(x => x.CreatedBy == currentUserName);

            
            //var currentUser = User.Identity.GetUserName();
            //var JobList = db.JobMasters.ToList().Select(e => new JobViewModel { JobName = e.JobName,  Quota =e.Quota, Description = e.Description, CreatedDate = e.DateTime, CurrentApplicant = e.CurrentApplicant, Status = e.Status })
            //    .Where(e => e.CreatedBy == currentUser);

            return PartialView("~/Views/Job/Index.cshtml", y);
        }

        public PartialViewResult MessageIndexContent()
        {
            MessageMaster x = new MessageMaster();
            x.From = User.Identity.Name;

            return PartialView("/Views/Message/Index.cshtml", x);

        }

        public PartialViewResult ApplicantContent()
        {
            var x = db.ApplicantsModels;
  
            return PartialView("/Views/ApplicantsModels/Index.cshtml", x);

        }

        // GET: Job
        public ActionResult Index()
        {

            var currentUserName = User.Identity.GetUserName();
            var model = db.JobMasters;

            // var y = model.Where(x => x.CreatedBy == currentUserName); // untuk Get Current applicant refer ke JobMastersController line 25
            var y = model.ToList().Select(joblist => new JobMaster {
                Id = joblist.Id,
                JobName = joblist.JobName,
                Image = joblist.Image,
                AvailablePosition = joblist.AvailablePosition,
                Description = joblist.Description,
                CreatedBy = joblist.CreatedBy,
                DateTime = joblist.DateTime,
                CurrentApplicant = db.ApplicantsModels.Where(am => am.JobId == joblist.Id).Count(),
                JobStatus = joblist.JobStatus,
                ApprovalStatus = joblist.ApprovalStatus,
                ExpiredDate = joblist.ExpiredDate,
                Category = joblist.Category
            }).Where(job => job.CreatedBy == currentUserName);
            
            
            //var currentUser = User.Identity.GetUserName();
            //var JobList = db.JobMasters.ToList().Select(e => new JobViewModel { JobName = e.JobName, Quota = e.Quota, Description = e.Description, CreatedDate = e.DateTime, CurrentApplicant = e.CurrentApplicant, Status = e.Status })
            //    .Where(e => e.CreatedBy == currentUser);

            return View(y);

        }

        // GET: Job/Details/5
        public ActionResult Details(int? id)
        {
            JobMaster jobMaster = db.JobMasters.Find(id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            if (jobMaster == null)
            {
                return HttpNotFound();
            }
            return View(jobMaster);
        }

        // GET: Job/Create
        public ActionResult Create()
        {
            ViewBag.Category = (from list in db.CategoryMaster
                 select new SelectListItem
                 {
                     Text = list.CategoryName,
                     Value = list.Id.ToString()
                 }).ToList();

            //ViewBag.Category = new SelectList(db.JobMasters
            //    .Select(category => category.Category), "Category");
            return View();
        }

        // POST: Job/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,JobName,Image,Quota,Description,CreatedBy,DateTime,CurrentApplicant,Status,ExpiredDate,CategoryName")] JobMaster jobMaster, HttpPostedFileBase image)
        {
            
            //JobMaster x = new JobMaster();

            if (image.ContentLength > 0)
            {
                CloudBlobContainer blobContainer = _blobStorageService.GetCloudBlobContainer();
                CloudBlockBlob blob = blobContainer.GetBlockBlobReference(image.FileName);
                blob.UploadFromStream(image.InputStream);
                var uri = blob.Uri.AbsoluteUri;
                jobMaster.Image = uri;

            }


            if (ModelState.IsValid)
            {
                jobMaster.CreatedBy = User.Identity.Name;
               
                db.JobMasters.Add(jobMaster);
                db.SaveChanges();
                //return RedirectToAction("Details");
            }
            else //abis if di guard jangan langsung masuk RedirectToAction, errornya jadi gak ketangkep
            {
                Response.StatusCode = 400;
                return Content("Model State is Invalid! Please re-check your form!");
            }

            //return View(jobMaster);
            return RedirectToAction("Details", new { id = jobMaster.Id });
        }

        // GET: Job/Edit/5
        public ActionResult Edit(int? id, HttpPostedFileBase image)
        {
            JobMaster jobMaster = db.JobMasters.Find(id);
         
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            if (jobMaster == null)
            {
                return HttpNotFound();
            }

            return View(jobMaster);
        }

        // POST: Job/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,JobName,Image,Quota,Description,CreatedBy,DateTime,CurrentApplicant,Status,ExpiredDate,CategoryId")] JobMaster jobMaster, HttpPostedFileBase image)
        {

            if (image.ContentLength > 0)
            {
                CloudBlobContainer blobContainer = _blobStorageService.GetCloudBlobContainer();
                CloudBlockBlob blob = blobContainer.GetBlockBlobReference(image.FileName);
                blob.UploadFromStream(image.InputStream);
                var uri = blob.Uri.AbsoluteUri;
                jobMaster.Image = uri;

            }

            if (ModelState.IsValid)
            {
                db.Entry(jobMaster).State = EntityState.Modified;
                db.SaveChanges();
                //return RedirectToAction("Index");
            }
            else
            {
                Response.StatusCode = 400;
                return Content("Model State is Invalid! Please re-check your form!");
            }
            //return View(jobMaster);
            return RedirectToAction("Details", new { id=jobMaster.Id });
        }

        // GET: Job/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobMaster jobMaster = db.JobMasters.Find(id);
            if (jobMaster == null)
            {
                return HttpNotFound();
            }
            return View(jobMaster);
        }

        // POST: Job/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JobMaster jobMaster = db.JobMasters.Find(id);
            db.JobMasters.Remove(jobMaster);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UploadAzure()
        {
            CloudBlobContainer blobContainer = _blobStorageService.GetCloudBlobContainer();
            List<string> blobs = new List<string>();
            foreach (var blobItem in blobContainer.ListBlobs())
            {
                blobs.Add(blobItem.Uri.ToString());
            }
            return View(blobs);
        }

        [HttpPost]
        public ActionResult UploadAzure(HttpPostedFileBase image)
        {
            if (image.ContentLength > 0)
            {
                CloudBlobContainer blobContainer = _blobStorageService.GetCloudBlobContainer();
                CloudBlockBlob blob = blobContainer.GetBlockBlobReference(image.FileName);
                blob.UploadFromStream(image.InputStream);
                var uri= blob.Uri.AbsoluteUri;
            }

            return RedirectToAction("UploadAzure");
        }

        [HttpPost]
        public ActionResult DeleteBlob(string Name)
        {
            Uri uri = new Uri(Name);
            string filename = System.IO.Path.GetFileName(uri.LocalPath);

            CloudBlobContainer blobContainer = _blobStorageService.GetCloudBlobContainer();
            CloudBlockBlob blob = blobContainer.GetBlockBlobReference(filename);

            blob.Delete();

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
