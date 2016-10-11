﻿using PurwadhikaWebApplication.Models;
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
    [Authorize]
    public class MessageController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Message
        public ActionResult Compose()
        {
            var model = new MessageMaster();
            model.From = User.Identity.Name;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Compose(MessageMaster MessageMaster)
        {
            if (ModelState.IsValid)
            {
                db.MessageMasters.Add(MessageMaster);
                db.SaveChanges();
                return new JavaScriptResult { Script = "alert('Message Sent Succesfully');" };

            }
            return View(MessageMaster);
        }

        public ActionResult Inbox()
        {
            
            var db = new ApplicationDbContext();
            string currentUserName = User.Identity.GetUserName();
           
            var messageList = db.MessageMasters.ToList()
                .Select(e => new MessageViewModel { From = e.From, To = e.To, Subject = e.Subject, Message = e.Message, DateTime = e.DateTime })
                .Where(e => e.To == currentUserName);    
                     
            return View(messageList);
     
        }

        public ActionResult SentBox()
        {
            var db = new ApplicationDbContext();
            string currentUserName = User.Identity.GetUserName();

            var SentList = db.MessageMasters.ToList()
                .Select(e => new MessageViewModel { From = e.From, To = e.To, Subject = e.Subject, Message = e.Message, DateTime = e.DateTime })
                .Where(e => e.From == currentUserName);
            return View(SentList);
        }

        public JsonResult GetUsers(string term)
        {
            List<string> users;
            users = db.Users.Where(x => x.UserName.StartsWith(term))
                .Select(y => y.UserName).ToList();

            return Json(users, JsonRequestBehavior.AllowGet);
        }


        //[HttpPost]
        //public JsonResult AutoCompleteSuggestion(string searchstring)
        //{
        //    var suggestion = from s in db.Users
        //                     select s.Email;
        //    var namelist = suggestion.Where(n => n.ToLower().StartsWith(searchstring.ToLower()));
        //    return Json(namelist, JsonRequestBehavior.AllowGet);
        //}s


    }
}