using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
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

        Cloudinary cloudinary = new Cloudinary(new Account(
             "mikiko16",
             "686516265985614",
             "cyjH_KBR9Djp3oOQhUWGcKr3FWg"));
        public AdService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<Ad> Delete(string id)
        {
            var pic = db.Ad.FirstOrDefault(x => x.Id == id);

            string input = pic.Link;
            int index = input.LastIndexOf("/") + 1;
            if (index > 0)
                input = input.Substring(index, input.Length - index - 4);

            cloudinary.DeleteResourcesByTag(input);

            var ad = db.Ad.FirstOrDefault(x => x.Id == id);
            db.Ad.Remove(ad);
            db.SaveChanges();

            return db.Ad.ToList();
        }

        public async Task<IEnumerable<Ad>> GetAllAds()
        {
            return db.Ad.ToList();
        }

        public Ad UploadProfilePicture(Ad model, string id)
        {
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
    }
}
