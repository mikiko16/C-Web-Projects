using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SharedTrip.Models
{
    public class UserTrip
    {
        [ForeignKey(nameof(User)), Required]
        public string UserId { get; set; }

        public User User { get; set; }

        public Trip Trip { get; set; }

        [ForeignKey(nameof(Trip)), Required]
        public string TripId { get; set; }
    }
}
