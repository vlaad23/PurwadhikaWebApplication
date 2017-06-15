using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PurwadhikaWebApplication.Models
{
    public class TranscriptsModel
    {
        public int id { get; set; }
        public string StudentId { get; set; }
        [Display(Name = "File Name")]
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        [ForeignKey("StudentId")]
        public virtual ApplicationUser Students { get; set; }

    }
}