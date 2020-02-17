using SharedTrip.Services;
using SharedTrip.ViewModels.Trips;
using SIS.HTTP;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrip.Controllers
{
    public class TripsController : Controller
    {
        private readonly ITripsService tripsService;

        public TripsController(ITripsService tripsService)
        {
            this.tripsService = tripsService;
        }
        public HttpResponse Add()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(AddTripsViewModel input)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }
            tripsService.CreateTrip(input.StartPoint, input.EndPoint, input.DepartureTime, input.ImagePath, input.Seats, input.Description);

            return this.Redirect("/");
        }

        public HttpResponse All()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var viewModel = new AllTripsViewModel
            {
                Trips = this.tripsService.AllTrips()
            };

            return this.View(viewModel);
        }

        public HttpResponse Details(string id)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }
            var viewDetailsModel = tripsService.GetDetails(id);

            return this.View(viewDetailsModel);
        }

        public HttpResponse AddUserToTrip(string id)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }
            string userId = this.Request.SessionData["UserId"];

            this.tripsService.AddUserToTrip(id, userId);

            return this.Redirect("/Trips/All");
        }
    }
}
