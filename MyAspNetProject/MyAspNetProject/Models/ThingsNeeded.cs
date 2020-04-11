using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyAspNetProject.Models
{
    public class ThingsNeeded
    {
        public ThingsNeeded()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string TeamBuildingId { get; set; }

        public string UserAppId { get; set; }
    }
}
