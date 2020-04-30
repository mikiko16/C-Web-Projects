using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyAspNetProject.Data;
using MyAspNetProject.models;
using MyAspNetProject.Models;
using MyAspNetProject.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyAspNetProject.Services
{
    public class TeamBuildingService : ITeamBuildingService
    {
        private readonly ApplicationDbContext db;

        public TeamBuildingService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<ActionResult<TeamBuilding>> CreateTeamBuilding(string companyName, TeamBuilding model)
        {
            var teamBuilding = new TeamBuilding
            {
                CompanyName = companyName,
                Location = model.Location,
                CreatorId = model.CreatorId,
                Date = model.Date
            };

            var team = db.TeamBuilding.Add(teamBuilding);
            await db.SaveChangesAsync();

            return teamBuilding;
        }

        public IEnumerable<TeamBuilding> GetActiveTeambuildings(UserApp user)
        {
            return db.TeamBuilding.Where(x => x.CompanyName == user.CompanyName && x.Date > DateTime.Now);
        }

        public IEnumerable<TeamBuilding> GetPastTeambuildings(UserApp user)
        {
            return db.TeamBuilding.Where(x => x.CompanyName == user.CompanyName && x.Date < DateTime.Now);
        }

        public TeamBuilding GetTeambuildingById(string id)
        {
            return db.TeamBuilding.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Pictures> GetTeambuildingPics(string id)
        {
            return db.Pictures.Where(x => x.TeamBuildingId == id);
        }
    }
}
