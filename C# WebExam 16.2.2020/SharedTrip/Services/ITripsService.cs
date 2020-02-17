using SharedTrip.ViewModels.Trips;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrip.Services
{
    public interface ITripsService
    {
        public void CreateTrip(string startpoint, string endpoint, DateTime departureTime, string imagePath, int seats, string description);

        IEnumerable<TripsInfoViewModel> AllTrips();

        public DetailsTripViewModel GetDetails(string id);

        void AddUserToTrip(string TripId, string userId);
    }
}
