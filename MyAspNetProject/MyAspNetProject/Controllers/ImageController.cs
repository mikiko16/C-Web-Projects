﻿//using CloudinaryDotNet;
//using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        private readonly IImageService imageService;

        Pictures picture = new Pictures();
        public ImageController(IImageService imageService)
        {
            this.imageService = imageService;
        }

        [HttpPost]
        [Authorize(Policy = "ApiUser")]
        [Route("uploadAdImage")]
        public async Task<string> UploadAdImage(IFormFile Image)
        {
            if (Image == null || Image.Length == 0)
                throw new Exception("Please select profile picture");

            string pic = await imageService.UploadAdPicture(Image);
            return pic;
        }

        [HttpPost]
        [Authorize(Policy = "ApiUser")]
        [Route("uploadTeamImage")]
        public async Task<IEnumerable<Pictures>> UploadTeamgImage(IFormFile Image, IFormCollection Id)
        {
            if (Image == null || Image.Length == 0)
                throw new Exception("Please select profile picture");

            var pictures = await imageService.UploadTeamPicture(Image, Id);

            return imageService.GetAllPictures(pictures);
        }
    }
}
