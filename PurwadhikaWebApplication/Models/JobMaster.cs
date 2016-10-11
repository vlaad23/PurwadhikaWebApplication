using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PurwadhikaWebApplication.Models
{
    public class JobMaster
    {
        public int Id { get; set; }

        public string JobName { get; set; }

        public byte[] Image { get; set; }

        public int Quota { get; set; }

        public string Description { get; set; }

        public string CreatedBy { get; set; }

        public DateTime DateTime { get; set; } = DateTime.Now;

        public int CurrentApplicant { get; set; }
    }
}