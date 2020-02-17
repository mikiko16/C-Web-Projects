using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrip.ViewModels.Trips
{
    public class TripsInfoViewModel
    {
        public string Id { get; set; }

        public string StartPoint { get; set; }

        public string EndPoint { get; set; }

        public DateTime DepartureTime { get; set; }

        public int Seats { get; set; }
    }
}
