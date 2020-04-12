using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyAspNetProject.Models
{
    public class Ad
    {
        public Ad()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        [Required]
        public string Id { get; set; }

        public string UserId { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public string Title { get; set; }

        public string Link{ get; set; }
    }
}
