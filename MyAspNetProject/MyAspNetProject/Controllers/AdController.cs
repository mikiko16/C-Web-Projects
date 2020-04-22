using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAspNetProject.Data;
using MyAspNetProject.Models;
using MyAspNetProject.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyAspNetProject.Controllers
{
    [ApiController]
    [EnableCors("AllowAll")]
    [Route("ads")]
    public class AdController
    {
        private readonly IAdService adService;
        private readonly ClaimsPrincipal _caller;

        public AdController(IHttpContextAccessor httpContextAccessor,
                            IAdService adService)
        {
            this.adService = adService;
            this._caller = httpContextAccessor.HttpContext.User;
        }

       [HttpPost]
       [Authorize(Policy = "ApiUser")]
       [Route("createAd")]
       public Ad UploadProfilePicture(Ad model)
       {
            var id = _caller.Claims.Single(c => c.Type == "id").Value;

            return this.adService.UploadProfilePicture(model, id);
       }

        [HttpGet]
        [Authorize(Policy = "ApiUser")]
        [Route("getAllAds")]
        public async Task<IEnumerable<Ad>> GetAllAds()
        {
            return await adService.GetAllAds();
        }

        [HttpDelete]
        [Authorize(Policy = "ApiUser")]
        [Route("deleteAd/{id}")]
        public async Task<IEnumerable<Ad>> delete(string id)
        {
            return await adService.Delete(id);
        }
    }
}
