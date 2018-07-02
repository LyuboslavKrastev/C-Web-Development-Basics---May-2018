using ChushkaApp.App.Models.BindingModels;
using ChushkaApp.App.Models.ViewModels;
using ChushkaApp.Models;
using Microsoft.EntityFrameworkCore;
using SoftUni.WebServer.Mvc.Attributes.HttpMethods;
using SoftUni.WebServer.Mvc.Attributes.Security;
using SoftUni.WebServer.Mvc.Interfaces;
using System;
using System.Linq;

namespace ChushkaApp.App.Controllers
{
    public class ProductsController : BaseController
    {
        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            if (!this.IsAdmin)
            {
                return this.RedirectToHome();
            }

            return this.View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(ProductCreatingBindingModel model)
        {
            if (!this.IsValidModel(model))
            {
                this.BuildErrorView();
                return this.View();
            }

            var productTypeId = Context.ProductTypes.FirstOrDefault(p => p.Name == model.Type).Id;


            var product = new Product()
            {
                Name = model.Name,
                Price = model.Price,
                Description = model.Description,
                ProductTypeId = productTypeId
            };

            using (this.Context)
            {

                this.Context.Products.Add(product);
                this.Context.SaveChanges();
            }

            return this.RedirectToHome();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Details(int id)
        {
            if (!this.IsAdmin)
            {
                this.ViewData["adminEditButton"] = string.Empty;
                this.ViewData["adminDeleteButton"] = string.Empty;
            }
            else
            {
                this.ViewData["adminEditButton"] = @"<a class=""btn chushka-bg-color"" href=""/products/Edit/{{{productId}}}"">Edit</a>";
                this.ViewData["adminDeleteButton"] = @"<a class=""btn chushka-bg-color"" href=""/products/Delete/{{{productId}}}"">Delete</a>";
            }
            using (this.Context)
            {
                var product = this.Context.Products.Include(p => p.Type)
                    .Where(p => p.Id == id).FirstOrDefault();

                if (product == null)
                {
                    this.ViewData["error"] = "The requested product does not exist";
                    return this.RedirectToHome();
                }

                var model = new ProductDetailsViewModel()
                {
                    Name = product.Name,
                    Price = product.Price,
                    Description = product.Description,
                    Type = product.Type.Name.ToString()
                };


                this.ViewData["name"] = model.Name;
                this.ViewData["description"] = model.Description;
                this.ViewData["price"] = $"${model.Price.ToString()}";
                this.ViewData["type"] = model.Type;
                this.ViewData["productId"] = $"?id={product.Id}";


                return this.View();
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult Order(int id)
        {
            var order = new Order()
            {
                Id = Guid.NewGuid().ToString(),
                ClientId = this.User.Id,
                ProductId = id,
                OrderedOn = DateTime.Now
            };

            Context.Orders.Add(order);
            Context.SaveChanges();

            this.ViewData["user"] = User.Name;
            this.ViewData["product"] = Context.Products.FirstOrDefault(p => p.Id == id).Name;

            return this.RedirectToHome();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Delete(int id)
        {
            if (!this.IsAdmin)
            {
                return this.RedirectToHome();
            }
            var product = this.Context.Products.FirstOrDefault(p => p.Id == id);

            Context.Products.Remove(product);
            Context.SaveChanges();

            return this.RedirectToHome();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Edit(int id)
        {
            if (!this.IsAdmin)
            {
                return this.RedirectToHome();
            }

            var product = this.Context.Products.FirstOrDefault(p => p.Id == id);

            this.ViewData["name"] = product.Name;
            this.ViewData["price"] = product.Price.ToString();
            this.ViewData["description"] = product.Description;
            this.ViewData["productId"] = $"{product.Id}";

            return this.View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, ProductCreatingBindingModel model)
        {
            if (!this.IsAdmin)
            {
                return this.RedirectToHome();
            }

            if (!this.IsValidModel(model))
            {
                this.BuildErrorView();
                return this.View();
            }

            var product = this.Context.Products.FirstOrDefault(p => p.Id == id);


            product.Name = model.Name;
            product.Price = model.Price;
            product.Description = model.Description;

            this.Context.SaveChanges();

            return this.RedirectToHome();
        }
    }
}
