using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using MyAspNetProject.Data;
using MyAspNetProject.models;
using MyAspNetProject.Models;
using MyAspNetProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace XUnitTests
{
    public class TeamBuildingTest
    {
        [Fact]
        public void CreateTeambuildingTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>();
            //.UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new ApplicationDbContext(options.Options);
            var TeamBuildingService = new TeamBuildingService(repository);

            var teamBuilding = new TeamBuilding
            {
                CompanyName = "IMI",
                Location = "Haskovo",
                CreatorId = "test1",
                Date = DateTime.Now
            };

            var result = TeamBuildingService.CreateTeamBuilding("IMI", teamBuilding);

            Assert.Equal("Haskovo", result.Result.Value.Location);
            Assert.Equal("IMI", result.Result.Value.CompanyName);

            var teambuild = repository.TeamBuilding.FirstOrDefault(x => x.CreatorId == "test1");
            repository.TeamBuilding.Remove(teambuild);
            repository.SaveChanges();
        }

        [Fact]
        public void GetTeamBuildingByIdTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>();

            var repository = new ApplicationDbContext(options.Options);
            var TeamBuildingService = new TeamBuildingService(repository);

            var result = TeamBuildingService.GetTeambuildingById("ccd88b13-f930-4039-9c26-f226b078028a");

            Assert.Equal("f349d29f-8c94-46dd-a1d3-bd5ee762062a", result.CreatorId);
            Assert.Equal("Burgas", result.Location);
        }
    }
}
