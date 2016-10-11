using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PurwadhikaWebApplication.Models
{
    public class JobViewModel
    {
        public int Id { get; set; }

        public string JobName { get; set; }

        public byte[] Image { get; set; }

        public int Quota { get; set; }

        public string Description { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }
        public int CurrentApplicant { get; set; }
    }
}