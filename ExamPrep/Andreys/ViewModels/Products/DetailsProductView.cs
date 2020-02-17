using Andreys.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Andreys.ViewModels.Products
{
    public class DetailsProductView
    {
        public string Name { get; set; }

        public Category Category { get; set; }

        public Gender Gender { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}
