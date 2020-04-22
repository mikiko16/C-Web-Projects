using MyAspNetProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyAspNetProject.Services.Contracts
{
    public interface IThingService
    {
        public IEnumerable<ThingsNeeded> CreateThing(ThingsNeeded model);

        public IEnumerable<ThingsNeeded> GetThing(string id);
    }
}
