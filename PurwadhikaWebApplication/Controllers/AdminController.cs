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

namespace PurwadhikaWebApplication.Controllers
{
    public class AdminController : Controller
    {
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
        public ActionResult UserIndex()
        {
            var db = new ApplicationDbContext();
            var userList = db.Users.ToList().Select(e => new UserIndexViewModel { Username = e.UserName, AccountStatus = e.AccountStatus, Role = UserManager.GetRoles(e.Id).FirstOrDefault() });
            
            return View(userList);
        }

        public ActionResult CurrateJob()
        {
            var db = new ApplicationDbContext();
            string currentUserName = User.Identity.GetUserName();

            var AllJobList = db.JobMasters.ToList()
                .Select(e => new JobViewModel { JobName = e.JobName, Quota = e.Quota, Description = e.Description, CreatedBy = e.CreatedBy, CreatedDate = e.DateTime });

            return View(AllJobList);

        }
    }
}