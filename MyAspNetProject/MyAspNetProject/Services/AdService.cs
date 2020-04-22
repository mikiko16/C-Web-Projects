using MyAspNetProject.Data;
using MyAspNetProject.Models;
using MyAspNetProject.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyAspNetProject.Services
{
    public class AdService : IAdService
    {
        private readonly ApplicationDbContext db;

        public AdService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<Ad>> Delete(string id)
        {
            var ad = db.Ad.FirstOrDefault(x => x.Id == id);
            db.Ad.Remove(ad);
            await db.SaveChangesAsync();

            return db.Ad.ToList();
        }

        public async Task<IEnumerable<Ad>> GetAllAds()
        {
            return db.Ad.ToList();
        }

        public async Task<Ad> UploadProfilePicture(Ad model, string id)
        {
            Ad ad = new Ad
            {
                UserId = id,
                Link = model.Link,
                Title = model.Title,
                Text = model.Text
            };

            db.Ad.Add(ad);
            await db.SaveChangesAsync();

            return ad;
        }
    }
}
