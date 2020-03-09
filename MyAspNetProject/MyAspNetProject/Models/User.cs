using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAspNetProject.Models
{
    public class User : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string CompanyName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }

        [Required]
        public bool isActive => false;
    }
}
