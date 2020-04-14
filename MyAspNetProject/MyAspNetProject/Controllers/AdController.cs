using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAspNetProject.Data;
using MyAspNetProject.Models;
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
        private readonly ApplicationDbContext db;
        private readonly ClaimsPrincipal _caller;

        public AdController(ApplicationDbContext db
                            , IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            this._caller = httpContextAccessor.HttpContext.User;
        }

       [HttpPost]
       [Authorize(Policy = "ApiUser")]
       [Route("createAd")]
       public async Task<Ad> UploadProfilePicture(Ad model)
       {
            var id = _caller.Claims.Single(c => c.Type == "id").Value;

           Ad ad = new Ad
           {
               UserId = id,
               Link = model.Link,
               Title = model.Title,
               Text = model.Text
           };

            db.Ad.Add(ad);
            db.SaveChanges();

            return ad;
       }

        [HttpGet]
        [Authorize(Policy = "ApiUser")]
        [Route("getAllAds")]
        public async Task<IEnumerable<Ad>> GetAllAds()
        {
            return db.Ad.ToList();
        }
    }
}
