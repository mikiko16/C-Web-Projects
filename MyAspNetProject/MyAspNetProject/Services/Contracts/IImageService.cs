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
        public Task<string> UploadAdPicture(IFormFile Image);

        public Task<string> UploadTeamPicture(IFormFile Image, IFormCollection data);

        public Task<ImageUploadResult> UploadPicture(IFormFile Image);
    }
}
