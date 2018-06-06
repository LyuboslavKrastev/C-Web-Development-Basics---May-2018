namespace CustomWebServer.ByTheCakeApplication.Services
{
    using Data;
    using Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Services.Contracts;
    using ViewModels.Orders;
    using ViewModels.Products;

    public class ShoppingService : IShoppingService
    {
        public void CreateOrder(int userId, IEnumerable<int> productIds)
        {
            using (var db = new ByTheCakeDbContext())
            {
                var order = new Order
                {
                    UserId = userId,
                    CreationDate = DateTime.UtcNow,
                    Products = productIds
                        .Select(id => new OrderProduct
                        {
                            ProductId = id
                        })
                        .ToList()
                };

                db.Add(order);
                db.SaveChanges();
            }
        }

        public OrderListingViewModel GetByOrderId(int orderId)
        {
            using (var db = new ByTheCakeDbContext())
            {
                var order = db
                .Orders
                .Where(o => o.Id == orderId)
                .Select(o => new OrderListingViewModel
                {
                    Id = o.Id,
                    CreationDate = o.CreationDate,
                    Products = o.Products
                        .Select(op => new ProductDetailsViewModel
                        {
                            Id = op.ProductId,
                            Name = op.Product.Name,
                            ImageUrl = op.Product.ImageUrl,
                            Price = op.Product.Price
                        }).ToList()
                })
                .FirstOrDefault();

                return order;
            }
        }

        public IEnumerable<OrderListingViewModel> GetByUserId(int? userId)
        {
            using (var db = new ByTheCakeDbContext())
            {
                var orders = db
                .Orders
                .Where(o => o.UserId == userId)
                .Select(o => new OrderListingViewModel
                {
                    Id = o.Id,
                    CreationDate = o.CreationDate,
                    TotalSum = o.Products.
                        Select(p => p.Product.Price).Sum(),
                    Products = o.Products
                        .Select(op => new ProductDetailsViewModel
                        {
                            Id = op.ProductId,
                            Name = op.Product.Name,
                            ImageUrl = op.Product.ImageUrl,
                            Price = op.Product.Price
                        })
                        .ToList()
                }).ToList();

                return orders;
            }
        }
    }
}
