using PurwadhikaWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Net;

namespace PurwadhikaWebApplication.Controllers
{

    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: Admin
        public ActionResult Index()
        {
            var userList = db.Users.ToList().Select(e => new UserIndexViewModel { Username = e.UserName, AccountStatus = e.AccountStatus, Role = UserManager.GetRoles(e.Id).FirstOrDefault() });

            return View(userList);
        }

        public ActionResult CurrateJob()
        {

            //string currentUserName = User.Identity.GetUserName();

            //var AllJobList = db.JobMasters.ToList()
            //    .Select(e => new JobViewModel { JobName = e.JobName, Quota = e.Quota, Description = e.Description, CreatedBy = e.CreatedBy, CreatedDate = e.DateTime, ExpiredDate = e.ExpiredDate, Status = e.Status });

            var model = db.JobMasters;
            return View(model);

        }

        public PartialViewResult UserIndexContent()
        {
            //List<UserIndexViewModel> x = new List<UserIndexViewModel>();
            var userList = db.Users.ToList().Select(e => new UserIndexViewModel { Username = e.UserName, AccountStatus = e.AccountStatus, Role = UserManager.GetRoles(e.Id).FirstOrDefault() });
            //if (ModelState.IsValid)
            //{
            //    ModelState.Clear();
            //}
            return PartialView("~/Views/Admin/UserIndex.cshtml", userList);
        }

        public PartialViewResult CurateJobContent()
        {
            var x = db.JobMasters;
            //if (ModelState.IsValid)
            //{
            //    ModelState.Clear();
            //}
            return PartialView("~/Views/Admin/CurrateJob.cshtml", x);

        }

        public PartialViewResult content03()
        {
            MessageMaster x = new MessageMaster();
            x.From = User.Identity.Name;
            //string currentUserName = User.Identity.GetUserName();
            //var messageList = db.MessageMasters.ToList()
            //    .Select(e => new MessageViewModel { From = e.From, To = e.To, Subject = e.Subject, Message = e.Message, DateTime = e.DateTime })
            //    .Where(e => e.To == currentUserName);


            //if (ModelState.IsValid)
            //{
            //    ModelState.Clear();
            //}
            return PartialView("/Views/Message/Index.cshtml", x);

        }

    }
}