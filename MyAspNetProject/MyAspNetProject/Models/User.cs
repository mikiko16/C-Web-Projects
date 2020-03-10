using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
<<<<<<< HEAD
using System.Linq;
using System.Threading.Tasks;
=======
>>>>>>> parent of a44f7aa... Register and Login Works!!!

namespace MyAspNetProject.Models
{
    public class User
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
        }
<<<<<<< HEAD
=======

        [Required]
        public string FirstName { get; set; }
>>>>>>> parent of a44f7aa... Register and Login Works!!!

        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string CompanyName { get; set; }
    }
}
