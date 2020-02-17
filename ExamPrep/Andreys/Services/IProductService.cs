using Andreys.Models;
using Andreys.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace Andreys.Services
{
    public interface IProductService
    {
        void AddProduct(string name, string description, string imageUrl, Category category, Gender gender, decimal price);

        IEnumerable<ProductInfoViewModel> GetAll();

        DetailsProductView viewDetails(int id);
    }
}
