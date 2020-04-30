using Microsoft.EntityFrameworkCore;
using MyAspNetProject.Data;
using MyAspNetProject.Models;
using MyAspNetProject.Services;
using System;
using System.Linq;
using Xunit;

namespace XUnitTests
{
    public class AdServiceTest
    {
        [Fact]
        public void AdServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>();

            var repository = new ApplicationDbContext(options.Options);
            var AdService = new AdService(repository);

            var input = new Ad
            {
                Title = "The best Programmer",
                Text = "Miro",
                Link = "https://res.cloudinary.com/mikiko16/image/upload/v1587752889/ff2a8bd0-4384-4281-af28-5031d0b08960.jpeg.jpg"
            };

            var result = AdService.UploadProfilePicture(input, "3a45a7ca-cf04-42fd-b37f-a4db3c3759f5");
            var link = result.Link;
            var text = result.Text;
            var title = result.Title;

            Assert.Equal("https://res.cloudinary.com/mikiko16/image/upload/v1587752889/ff2a8bd0-4384-4281-af28-5031d0b08960.jpeg.jpg", link);
            Assert.Equal("Miro", text);
            Assert.Equal("The best Programmer", title);

            var ad = repository.Ad.FirstOrDefault(x => x.Title == input.Title);
            repository.Ad.Remove(ad);
            repository.SaveChanges();
        }

        [Fact]
        public void GetAllAdsServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>();

            var repository = new ApplicationDbContext(options.Options);
            var AdService = new AdService(repository);

            var result = AdService.GetAllAds();
            var allAds = result.Result.Count();

            Assert.Equal(repository.Ad.Count(), allAds);
        }
    }
}
