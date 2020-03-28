using MyAspNetProject.models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyAspNetProject.Models
{
    public class TeamBuilding
    {
        public TeamBuilding()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        [Required]
        public string Id { get; set; }

        [Required]
        public ThingsNeeded[] Things { get; set; }

        public Pictures[] PicturesIDs { get; set; }

        [Required]
        public UserApp[] Users { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string CreatorId { get; set; }
    }
}
