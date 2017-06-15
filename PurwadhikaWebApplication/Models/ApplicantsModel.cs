using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PurwadhikaWebApplication.Models
{
    public class ApplicantsModel
    {
        public int id { get; set; }

        public string StudentId { get; set; }
        public int JobId { get; set; }

        [ForeignKey("StudentId")]
        public virtual ApplicationUser Students { get; set; }
        [ForeignKey("JobId")]
        public virtual JobMaster Jobs { get; set; }
    }
}