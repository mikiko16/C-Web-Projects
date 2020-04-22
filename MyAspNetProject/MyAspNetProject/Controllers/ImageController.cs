using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAspNetProject.Data;
using MyAspNetProject.Models;
using MyAspNetProject.Services;
using MyAspNetProject.Services.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyAspNetProject.Controllers
{
    [ApiController]
    [EnableCors("AllowAll")]
    [Route("images")]
    public class ImageController
    {
        private readonly ApplicationDbContext db;
        private readonly IImageService imageService;

        public ImageController(ApplicationDbContext db,
                               IImageService imageService)
        {
            this.db = db;
            this.imageService = imageService;
        }

        [HttpPost]
        [Authorize(Policy = "ApiUser")]
        [Route("uploadAdImage")]
        public string UploadAdImage(IFormFile Image)
        {
            if (Image == null || Image.Length == 0)
                throw new Exception("Please select profile picture");

            return imageService.UploadAdPicture(Image);
            
           // var id = data["Id"];
            
           // Account account = new Account(
           //  "mikiko16",
           //  "686516265985614",
           //  "cyjH_KBR9Djp3oOQhUWGcKr3FWg");
           // 
           // Cloudinary cloudinary = new Cloudinary(account);
           // 
           // var folderName = Path.Combine("Resources", "ProfilePics");
           // var filePath = Path.Combine(Directory.GetCurrentDirectory(), folderName);
           // 
           // if (!Directory.Exists(filePath))
           // {
           //     Directory.CreateDirectory(filePath);
           // }
           // 
           // var picture = new Pictures();
           // 
           // var uniqueFileName = $"{picture.Id}.jpeg";
           // var dbPath = Path.Combine(folderName, uniqueFileName);
           // 
           // using (var fileStream = new FileStream(Path.Combine(filePath, uniqueFileName), FileMode.Create))
           // {
           //     await Image.CopyToAsync(fileStream);
           // }
           // 
           // var uploadParams = new ImageUploadParams()
           // {
           //     File = new FileDescription($"{dbPath}"),
           //     PublicId = picture.Id,
           //     Overwrite = true
           // };
           // 
           // var uploadResult = cloudinary.Upload(uploadParams);
           // 
           // picture.Link = uploadResult.SecureUri.ToString();
           // db.Pictures.Add(picture);
           // db.SaveChanges();
           // 
           // return picture.Link;

            //picture.Link = uploadResult.SecureUri.ToString();
            //picture.TeamBuildingId = id.ToString();
            //db.Pictures.Add(picture);
            //db.SaveChanges();
            //
            //return db.Pictures.Where(x => x.TeamBuildingId == id);
        }

        [HttpPost]
        [Authorize(Policy = "ApiUser")]
        [Route("uploadTeamImage")]
        public Task<IEnumerable<Pictures>> UploadTeamgImage(IFormFile Image, IFormCollection data)
        {
            if (Image == null || Image.Length == 0)
                throw new Exception("Please select profile picture");

            return imageService.UploadTeamPicture(Image, data);
        }
    }
}
