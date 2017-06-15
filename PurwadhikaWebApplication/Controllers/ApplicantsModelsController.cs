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

namespace PurwadhikaWebApplication.Controllers
{
    public class ApplicantsModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ApplicantsModels
        public ActionResult Index()
        {
            var currentUser = User.Identity.GetUserName();
            var applicantsModels = db.ApplicantsModels.Include(a => a.Jobs).Include(a => a.Students)
                .Where(a => a.Jobs.CreatedBy == currentUser);

            return View(applicantsModels.ToList());
        }

        // GET: ApplicantsModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicantsModel applicantsModel = db.ApplicantsModels.Find(id);
            if (applicantsModel == null)
            {
                return HttpNotFound();
            }
            return View(applicantsModel);
        }

        // GET: ApplicantsModels/Create
        public ActionResult Create()
        {
            ViewBag.JobId = new SelectList(db.JobMasters, "Id", "JobName");
            ViewBag.StudentId = new SelectList(db.Users, "Id", "Fullname");
            return View();
        }

        // POST: ApplicantsModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,StudentId,JobId")] ApplicantsModel applicantsModel)
        {
            var currentUser = User.Identity.GetUserName();
            if (ModelState.IsValid)
            {
                db.ApplicantsModels.Add(applicantsModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.JobId = new SelectList(db.JobMasters.Where(jm => jm.CreatedBy == currentUser), "Id", "JobName", applicantsModel.JobId);
            ViewBag.StudentId = new SelectList(db.Users, "Id", "Fullname", applicantsModel.StudentId);
            return View(applicantsModel);
        }

        // GET: ApplicantsModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicantsModel applicantsModel = db.ApplicantsModels.Find(id);
            if (applicantsModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.JobId = new SelectList(db.JobMasters, "Id", "JobName", applicantsModel.JobId);
            ViewBag.StudentId = new SelectList(db.Users, "Id", "Firstname", applicantsModel.StudentId);
            return View(applicantsModel);
        }

        // POST: ApplicantsModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,StudentId,JobId")] ApplicantsModel applicantsModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicantsModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.JobId = new SelectList(db.JobMasters, "Id", "JobName", applicantsModel.JobId);
            ViewBag.StudentId = new SelectList(db.Users, "Id", "Firstname", applicantsModel.StudentId);
            return View(applicantsModel);
        }

        // GET: ApplicantsModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicantsModel applicantsModel = db.ApplicantsModels.Find(id);
            if (applicantsModel == null)
            {
                return HttpNotFound();
            }
            return View(applicantsModel);
        }

        // POST: ApplicantsModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ApplicantsModel applicantsModel = db.ApplicantsModels.Find(id);
            db.ApplicantsModels.Remove(applicantsModel);
            db.SaveChanges();
            return RedirectToAction("Index");
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
