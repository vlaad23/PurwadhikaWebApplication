using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PurwadhikaWebApplication.Models
{
    public enum JobStatus
    {
        Open,
        Close
    }
    public enum ApprovalStatus
    {
        Pending, 
        Approved,
        Rejected
    }
    
    public class JobMaster
    {

        public int Id { get; set; }
        [Display(Name = "Job Name")]
        public string JobName { get; set; }

        public string Image { get; set; }

        public int AvailablePosition { get; set; }

        public string Description { get; set; }
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
        [Display(Name = "Created Date")]
        public DateTime DateTime { get; set; } = DateTime.Now;

        [Display(Name = "Current Applicant")]
        public int CurrentApplicant { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public ApprovalStatus ApprovalStatus { get; set; } = ApprovalStatus.Pending;

        public JobStatus JobStatus { get; set; } = JobStatus.Open;

        [Display(Name = "Expired Date")]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ExpiredDate { get; set; } = DateTime.Now;
       
        public int? CategoryId { get; set; }
       
        [ForeignKey("CategoryId")]
        public virtual CategoryMaster Category { get; set; }
    }

    
}