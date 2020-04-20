using MyAspNetProject.models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [NotMapped]
        public string[] Things { get; set; }

        public ThingsNeeded[] ThingsNeeded { get; set; }

        public ICollection<Pictures> PicturesIDs { get; set; }

        public UserApp[] Users { get; set; }

        [Required]
        public string Location { get; set; }

        public string CompanyName { get; set; }

        [Required]
        public string CreatorId { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
