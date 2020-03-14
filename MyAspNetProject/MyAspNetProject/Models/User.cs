using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyAspNetProject.models
{
    public class UserApp
    {
        public UserApp()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string CompanyName { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public bool IsAdmin { get; set; }
    }
}
