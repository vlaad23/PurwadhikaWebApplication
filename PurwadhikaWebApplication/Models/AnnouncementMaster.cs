using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PurwadhikaWebApplication.Models
{
    public class AnnouncementMaster
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Message { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}