using Andreys.Models;
using Andreys.Services;
using Andreys.ViewModels.Products;
using SIS.HTTP;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Andreys.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductService productService;

        public ProductsController(ProductService productService)
        {
            this.productService = productService;
        }
        public HttpResponse Add()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(AddProductViewModel input)
        {
            Category category = (Category)Enum.Parse(typeof(Category), input.Category, true);
            Gender gender = (Gender)Enum.Parse(typeof(Gender), input.Gender, true);

            this.productService.AddProduct(input.Name, input.Description, input.ImageUrl, category, gender, input.Price);

            return this.Redirect("/");
        }

        public HttpResponse Details(int id)
        {
            var details = this.productService.viewDetails(id);

            return this.View(details);
        }
    }
}
