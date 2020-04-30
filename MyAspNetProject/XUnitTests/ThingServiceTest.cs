using Microsoft.EntityFrameworkCore;
using MyAspNetProject.Data;
using MyAspNetProject.Models;
using MyAspNetProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace XUnitTests
{
    public class ThingServiceTest
    {
        [Fact]
        public void GetThingTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>();

            var repository = new ApplicationDbContext(options.Options);
            var ThingService = new ThingService(repository);

            var result = ThingService.GetThing("ccd88b13-f930-4039-9c26-f226b078028a");

            Assert.Equal("4c9fbfbb-9e0e-43d1-89f1-92c02a912b1d", result.ToList()[0].Id);
            Assert.Equal("bread", result.ToList()[0].Name);
        }

        [Fact]
        public void CreateThingTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>();

            var repository = new ApplicationDbContext(options.Options);
            var ThingService = new ThingService(repository);

            ThingsNeeded thing = new ThingsNeeded
            {
                Name = "Vodka",
                TeamBuildingId = "ccd88b13-f930-4039-9c26-f226b078028a",
                UserAppId = "30e89255-bb31-4e09-bdc6-86f38c52031f"
            };
            var result = ThingService.CreateThing(thing);

            Assert.Equal(2, result.Count());
            Assert.Equal("30e89255-bb31-4e09-bdc6-86f38c52031f", result.ToList()[0].UserAppId);

            var thingtodelete = repository.ThingsNedded.FirstOrDefault(x => x.UserAppId == "30e89255-bb31-4e09-bdc6-86f38c52031f" && x.Name == "Vodka");
            repository.ThingsNedded.Remove(thingtodelete);
            repository.SaveChanges();
        }
    }
}
