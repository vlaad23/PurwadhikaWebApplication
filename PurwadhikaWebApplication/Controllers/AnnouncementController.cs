using Microsoft.AspNet.Identity;
using PurwadhikaWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PurwadhikaWebApplication.Controllers
{
    public class AnnouncementController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Announcement
        public ActionResult Index()
        {
            var db = new ApplicationDbContext();
            string currentUserName = User.Identity.GetUserName();

            var AnnouncementList = db.AnnouncementMasters.ToList()
                .Select(e => new AnnouncementViewModel { Name = e.Name, From = e.From, To = e.To, Message = e.Message, DateTime = e.DateTime });

            return View(AnnouncementList);
        }

        public ActionResult Create()
        {
            var model = new AnnouncementMaster();
            model.From = User.Identity.Name;
            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AnnouncementMaster AnnouncementMaster)
        {
            if (ModelState.IsValid)
            {
                db.AnnouncementMasters.Add(AnnouncementMaster);
                db.SaveChanges();
                return new JavaScriptResult { Script = "alert('Announcement Sent Succesfully');" };

            }
            return View(AnnouncementMaster);
        }

    }
}