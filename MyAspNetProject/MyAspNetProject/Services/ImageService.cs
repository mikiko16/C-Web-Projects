using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
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
    public class ImageService : IImageService
    {
        Cloudinary cloudinary = new Cloudinary(new Account(
             "mikiko16",
             "686516265985614",
             "cyjH_KBR9Djp3oOQhUWGcKr3FWg"));

        Pictures picture = new Pictures();
        private readonly ApplicationDbContext db;

        public ImageService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<string> UploadAdPicture(IFormFile Image)
        {
            ImageUploadResult result = await this.UploadPicture(Image);

            picture.Link = result.SecureUri.ToString();
            db.Pictures.Add(picture);
            db.SaveChanges();

            return picture.Link;
        }

        public async Task<string> UploadTeamPicture(IFormFile Image, IFormCollection data)
        {
            var id = data["Id"];

            ImageUploadResult result = await this.UploadPicture(Image);

            picture.Link = result.SecureUri.ToString();
            picture.TeamBuildingId = id.ToString();
            db.Pictures.Add(picture);
            db.SaveChanges();
            
            return id;
        }

        public async Task<ImageUploadResult> UploadPicture(IFormFile Image)
        {
            var folderName = Path.Combine("Resources", "ProfilePics");
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            //var picture = new Pictures();

            var uniqueFileName = $"{picture.Id}.jpeg";
            var dbPath = Path.Combine(folderName, uniqueFileName);

            using (var fileStream = new FileStream(Path.Combine(filePath, uniqueFileName), FileMode.Create))
            {
                await Image.CopyToAsync(fileStream);
            }

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription($"{dbPath}"),
                PublicId = picture.Id,
                Overwrite = true
            };

            var uploadResult = cloudinary.Upload(uploadParams);

            return uploadResult;
        }
    }
}
