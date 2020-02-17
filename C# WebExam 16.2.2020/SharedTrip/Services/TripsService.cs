using Microsoft.AspNetCore.Mvc.RazorPages;
using SharedTrip.Models;
using SharedTrip.ViewModels.Trips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharedTrip.Services
{
    public class TripsService : ITripsService
    {
        private readonly ApplicationDbContext db;
        private readonly IUserService userService;

        public TripsService(ApplicationDbContext db, IUserService UserService)
        {
            this.db = db;
            userService = UserService;
        }

        public void AddUserToTrip(string TripId, string userId)
        {
            var AddToTrip = new UserTrip
            {
                TripId = TripId,
                UserId = userId
            };
            db.UserTrips.Add(AddToTrip);
            db.SaveChanges();
        }

        public IEnumerable<TripsInfoViewModel> AllTrips()
        {
            var allTrips = this.db.Trips.Select(x => new TripsInfoViewModel
            {
                StartPoint = x.StartPoint,
                EndPoint = x.EndPoint,
                Id = x.Id,
                DepartureTime = x.DepartureTime,
                Seats = x.Seats
            }).ToList();

            return allTrips;
        }

        public void CreateTrip(string startpoint, string endpoint, DateTime departureTime, string imagePath, int seats, string description)
        {
            var trip = new Trip
            {
                StartPoint = startpoint,
                EndPoint = endpoint,
                DepartureTime = departureTime,
                ImagePath = imagePath,
                Seats = seats,
                Description = description
            };
            db.Trips.Add(trip);
            db.SaveChanges();
        }

        public DetailsTripViewModel GetDetails(string id)
        {
            var trip = this.db.Trips.Where(x => x.Id == id);

            return trip.Select(x => new DetailsTripViewModel
                {
                    Id = x.Id,
                    StartPoint = x.StartPoint,
                    EndPoint = x.EndPoint,
                    DepartureTime = x.DepartureTime,
                    Seats = x.Seats,
                    Description = x.Description,
                    ImagePath = x.ImagePath
                }).FirstOrDefault();
        }
    }
}
