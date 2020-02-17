namespace SharedTrip
{
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using SharedTrip.Services;
    using SIS.HTTP;
    using SIS.MvcFramework;

    public class Startup : IMvcApplication
    {
        public void Configure(IList<Route> routeTable)
        {
            var db = new ApplicationDbContext();
            db.Database.Migrate();
            db.Database.EnsureCreated();
        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<IUserService, UserService>();
            serviceCollection.Add<ITripsService, TripsService>();
        }
    }
}
