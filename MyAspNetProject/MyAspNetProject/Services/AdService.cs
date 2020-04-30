using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using MyAspNetProject.Data;
using MyAspNetProject.Models;
using MyAspNetProject.Services.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
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


            var folderName = Path.Combine("Resources", "ProfilePics");
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            var dbPath = Path.Combine(filePath, input);
            File.Delete(dbPath);

            cloudinary.DeleteResourcesByTag(input);

            var picture = db.Pictures.FirstOrDefault(x => x.Link == pic.Link);
            db.Ad.Remove(pic);
            db.Pictures.Remove(picture);
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
