using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PurwadhikaWebApplication.Models
{
    public class UserViewModel
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Gender { get; set; }
        public string  Address { get; set; }
        public int Batch { get; set; }
        public string AccountPicture { get; set; }
        public string About { get; set; }
        public string Skills { get; set; }
        public string Experience { get; set; }

    }
}