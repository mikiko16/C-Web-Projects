﻿using Microsoft.AspNetCore.Mvc;
using MyAspNetProject.models;
using MyAspNetProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyAspNetProject.Services.Contracts
{
    public interface ITeamBuildingService
    {
        public Task<ActionResult<TeamBuilding>> CreateTeamBuilding(string companyName, TeamBuilding model);

        public IEnumerable<TeamBuilding> GetActiveTeambuildings(UserApp user);

        public IEnumerable<TeamBuilding> GetPastTeambuildings(UserApp user);

        public TeamBuilding GetTeambuildingById(string id);

        public IEnumerable<Pictures> GetTeambuildingPics(string id);
    }
}
