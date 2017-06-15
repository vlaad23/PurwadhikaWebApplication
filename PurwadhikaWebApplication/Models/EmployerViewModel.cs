using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PurwadhikaWebApplication.Models
{
    public class EmployerViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Role { get; set; }

        [Required]
        [EmailAddress]
        [Remote("UserAlreadyExistsAsync", "Account", ErrorMessage = "Email already used. Please enter a different email.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [Required] 
        public string Address { get; set; }
        public string AccountPicture { get; set; }
        [Required]
        [Display(Name = "Instance Name")]
        public string InstanceName { get; set; }
        [Required]
        public string Industry { get; set; }

    }
}