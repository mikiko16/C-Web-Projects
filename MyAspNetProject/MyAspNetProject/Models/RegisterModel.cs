using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyAspNetProject.Models
{
    public class RegisterModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string CompanyName { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public bool isActive => false;
    }
}
