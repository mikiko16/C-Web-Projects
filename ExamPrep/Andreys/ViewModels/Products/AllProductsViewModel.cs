using System;
using System.Collections.Generic;
using System.Text;

namespace Andreys.ViewModels.Products
{
    public class AllProductsViewModel
    {
        public IEnumerable<ProductInfoViewModel> Products { get; set; }
    }
}
