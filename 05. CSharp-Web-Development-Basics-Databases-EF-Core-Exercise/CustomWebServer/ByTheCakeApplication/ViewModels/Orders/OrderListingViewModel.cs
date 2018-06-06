using CustomWebServer.ByTheCakeApplication.ViewModels.Products;
using System;
using System.Collections.Generic;

namespace CustomWebServer.ByTheCakeApplication.ViewModels.Orders
{
    public class OrderListingViewModel
    {
        public int Id { get; set; }

        public DateTime CreationDate { get; set; }

        public decimal TotalSum { get; set; }

        public ICollection<ProductDetailsViewModel> Products { get; set; }
    }
}