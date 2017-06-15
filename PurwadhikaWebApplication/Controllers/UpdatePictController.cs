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
    public class UpdatePictController : Controller
    {
        BlobUserService _blobUserService = new BlobUserService();
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult UpdatePict()
        {
            return View();
        }
    }
}