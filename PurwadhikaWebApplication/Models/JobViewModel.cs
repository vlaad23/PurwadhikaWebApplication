using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PurwadhikaWebApplication.Models
{
    public class JobViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Job Name")]
        public string JobName { get; set; }

        public string Image { get; set; }

        public int Quota { get; set; }

        public string Description { get; set; }
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }
        [Display(Name = "Current Applicant")]
        public int CurrentApplicant { get; set; }
        public string Status { get; set; }
        [Display(Name = "Expired Date")]
        [DataType(DataType.Date)]
        public DateTime ExpiredDate { get; set; }
    }
}