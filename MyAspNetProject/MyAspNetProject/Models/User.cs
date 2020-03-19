using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyAspNetProject.models
{
    public class UserApp : IdentityUser
    {
        public UserApp()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string CompanyName { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}
