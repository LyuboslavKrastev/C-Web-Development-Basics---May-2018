namespace CustomWebServer.ByTheCakeApplication.Controllers
{
    using ViewModels.Orders;
    using Server.Http;
    using Server.Http.Contracts;
    using Server.Http.Response;
    using Services;
    using Services.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ViewModels;
    using ViewModels.Products;

    public class ShoppingController : BaseController
    {
        private readonly IUserService userService;
        private readonly IProductService productService;
        private readonly IShoppingService shoppingService;

        public ShoppingController()
        {
            this.userService = new UserService();
            this.productService = new ProductService();
            this.shoppingService = new ShoppingService();
        }

        public IHttpResponse AddToCart(IHttpRequest req)
        {
            var id = int.Parse(req.UrlParameters["id"]);

            var productExists = this.productService.Exists(id);

            if (!productExists)
            {
                return new NotFoundResponse();
            }

            var shoppingCart = req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);
            shoppingCart.ProductIds.Add(id);

            var redirectUrl = "/search";

            const string searchTermKey = "searchTerm";

            if (req.UrlParameters.ContainsKey(searchTermKey))
            {
                redirectUrl = $"{redirectUrl}?{searchTermKey}={req.UrlParameters[searchTermKey]}";
            }

            return new RedirectResponse(redirectUrl);
        }

        public IHttpResponse ShowCart(IHttpRequest req)
        {
            var shoppingCart = req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);

            if (!shoppingCart.ProductIds.Any())
            {
                this.ViewData["cartItems"] = "No items in your cart";
                this.ViewData["totalCost"] = "0.00";
            }
            else
            {
                var productsInCart = this.productService
                    .FindProductsInCart(shoppingCart.ProductIds);

                var items = productsInCart
                    .Select(pr => $"<div>{pr.Name} - ${pr.Price:F2}</div><br />");

                var totalPrice = productsInCart
                    .Sum(pr => pr.Price);
                
                this.ViewData["cartItems"] = string.Join(string.Empty, items);
                this.ViewData["totalCost"] = $"{totalPrice:F2}";
            }

            return this.FileViewResponse(@"shopping\cart");
        }

        public IHttpResponse FinishOrder(IHttpRequest req)
        {
            var username = req.Session.Get<string>(SessionStore.CurrentUserKey);
            var shoppingCart = req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);

            var userId = this.userService.GetUserId(username);
            if (userId == null)
            {
                throw new InvalidOperationException($"User {username} does not exist");
            }

            var productIds = shoppingCart.ProductIds;
            if (!productIds.Any())
            {
                return new RedirectResponse("/");
            }

            this.shoppingService.CreateOrder(userId.Value, productIds);

            shoppingCart.ProductIds.Clear();

            return this.FileViewResponse(@"shopping\finish-order");
        }

        public IHttpResponse UserOrders(IHttpRequest req)
        {
            var username = req.Session.Get<string>(SessionStore.CurrentUserKey);

            var userId = this.userService.GetUserId(username);

            var orders = this.shoppingService.GetByUserId(userId);

            var tableRows = this.CreateOrderTable(orders);

            this.ViewData["orders-rows"] = tableRows;

            return this.FileViewResponse(@"shopping\user-orders");
        }

        public IHttpResponse OrderDetails(IHttpRequest req)
        {
            if (!req.UrlParameters.ContainsKey("id"))
            {
                return new NotFoundResponse();
            }

            var orderId = req.UrlParameters["id"];

            if (orderId == null)
            {
                return new NotFoundResponse();
            }

            var orderDetails = this.shoppingService.GetByOrderId(int.Parse(orderId));

            var productsRows = this.CreateProductTable(orderDetails.Products);

            this.ViewData["creation-date"] = orderDetails.CreationDate.ToShortDateString();
            this.ViewData["products-rows"] = productsRows;
            this.ViewData["order-id"] = orderDetails.Id.ToString();

            return this.FileViewResponse(@"shopping\order-details");
        }


        private string CreateOrderTable(IEnumerable<OrderListingViewModel> orders)
        {
            var htmlTable = new StringBuilder();

            foreach (var order in orders.OrderByDescending(o => o.CreationDate))
            {

                htmlTable
                    .AppendLine($"<tr><td><a href='orderDetails/{order.Id}'>{order.Id}</a></td><td>{order.CreationDate}</td><td>${order.TotalSum}</td></tr>");
            }

            return htmlTable.ToString().TrimEnd();          
        }

        private string CreateProductTable(ICollection<ProductDetailsViewModel> products)
        {
            var htmlTable = new StringBuilder();

            foreach (var product in products.OrderBy(o => o.Price))
            {
                htmlTable
                    .AppendLine($"<tr><td><a href='productDetails/{product.Id}'>{product.Name}</a></td><td>${product.Price}</td></tr>");
            }

            return htmlTable.ToString().TrimEnd();       
        }
    }
}
