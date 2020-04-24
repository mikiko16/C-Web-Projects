using Microsoft.AspNetCore.Mvc;
using MyAspNetProject.Data;
using MyAspNetProject.Models;
using MyAspNetProject.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyAspNetProject.Services
{
    public class ThingService : IThingService
    {
        private readonly ApplicationDbContext db;

        public ThingService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IEnumerable<ThingsNeeded> CreateThing(ThingsNeeded model)
        {
            ThingsNeeded thing = new ThingsNeeded
            {
                Name = model.Name,
                TeamBuildingId = model.TeamBuildingId,
                UserAppId = model.UserAppId
            };

            db.ThingsNedded.Add(thing);
            db.SaveChanges();

            return db.ThingsNedded.Where(x => x.TeamBuildingId == model.TeamBuildingId && x.UserAppId == model.UserAppId);
        }

        public IEnumerable<ThingsNeeded> GetThing(string id)
        {
            return this.db.ThingsNedded.Where(x => x.TeamBuildingId == id);
        }
    }
}
