using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PurwadhikaWebApplication.Models
{
    public class CategoryMaster
    {
        [Key]
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string CategoryImage { get; set; }

    }
}