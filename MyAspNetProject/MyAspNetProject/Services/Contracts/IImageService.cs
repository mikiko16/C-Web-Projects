using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using MyAspNetProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyAspNetProject.Services.Contracts
{
    public interface IImageService
    {
        public string UploadAdPicture(IFormFile Image);

        public Task<IEnumerable<Pictures>> UploadTeamPicture(IFormFile Image, IFormCollection data);

        public ImageUploadResult UploadPicture(IFormFile Image);
    }
}
