﻿namespace Andreys.App
{
    using System.Collections.Generic;
    using Data;
    using SIS.MvcFramework;
    using SIS.HTTP;
    using Microsoft.EntityFrameworkCore;
    using Andreys.Services;

    public class Startup : IMvcApplication
    {
        public void Configure(IList<Route> serverRoutingTable)
        {
            using (var db = new AndreysDbContext())
            {
                db.Database.EnsureCreated();
                db.Database.Migrate();
            }
        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<IUserService, UserService>();
            serviceCollection.Add<IProductService, ProductService>();
        }
    }
}
