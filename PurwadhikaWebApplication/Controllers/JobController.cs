using Microsoft.AspNet.Identity;
using PurwadhikaWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PurwadhikaWebApplication.Controllers
{
    public class JobController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Job
        public ActionResult Index()
        {
            var db = new ApplicationDbContext();
            string currentUserName = User.Identity.GetUserName();

            var MyJobList = db.JobMasters.ToList()
                .Select(e => new JobViewModel { JobName = e.JobName, Quota = e.Quota, CurrentApplicant = e.CurrentApplicant ,Description = e.Description ,CreatedBy = e.CreatedBy, CreatedDate = e.DateTime })
                .Where(e => e.CreatedBy == currentUserName);

            return View(MyJobList);

        }
        public ActionResult Create()
        {
            var model = new JobMaster();
            model.CreatedBy = User.Identity.Name;
            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(JobMaster JobMaster)
        {
            if (ModelState.IsValid)
            {
                db.JobMasters.Add(JobMaster);
                db.SaveChanges();
                return new JavaScriptResult { Script = "alert('Announcement Sent Succesfully');" };

            }
            return View(JobMaster);
        }
    }
}