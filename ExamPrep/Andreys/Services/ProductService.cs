using Andreys.Data;
using Andreys.Models;
using Andreys.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andreys.Services
{
    public class ProductService : IProductService
    {
        private readonly AndreysDbContext db;
        private readonly Random random;

        public ProductService(AndreysDbContext db, Random random)
        {
            this.db = db;
            this.random = random;
        }


        public void AddProduct(string name, string description, string imageUrl, Category category, Gender gender, decimal price)
        {
            var product = new Product
            {
                Id = random.Next(0, 100),
                Name = name,
                Description = description,
                ImageUrl = imageUrl,
                Category = category,
                Gender = gender,
                Price = price
            };

            db.Products.Add(product);

            db.SaveChanges();
        }

        public IEnumerable<ProductInfoViewModel> GetAll()
        {
            var allProducts = db.Products.Select(x => new ProductInfoViewModel
            {
                Name = x.Name,
                ImageUrl = x.ImageUrl,
                Price = x.Price,
                Id = x.Id
            }).ToList();

            return allProducts;
        }

        public DetailsProductView viewDetails(int id)
        {
            var product = db.Products.Where(x => x.Id == id)
                .Select(x => new DetailsProductView
                {
                    Name = x.Name,
                    Category = x.Category,
                    Description = x.Description,
                    Gender = x.Gender,
                    Price = x.Price,
                    ImageUrl = x.ImageUrl
                }).FirstOrDefault();

            return product;
        }
    }
}
