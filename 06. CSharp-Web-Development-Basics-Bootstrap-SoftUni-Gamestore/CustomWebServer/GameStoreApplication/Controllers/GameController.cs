namespace CustomWebServer.GameStoreApplication.Controllers
{
    using Services.Contracts;
    using Server.Http.Contracts;
    using Services;
    using Server.Http.Response;
    using System.Text;
    using ViewModels;
    using System.Linq;
    using Helpers;

    public class GameController : BaseController
    {
        private IGameService games;

        public GameController(IHttpRequest request) 
            : base(request)
        {
            this.games = new GameService();          
        }

        public IHttpResponse Details(int id)
        {

            var product = this.games.FindById(id);

            if (product == null)
            {
                return new NotFoundResponse();
            }

            this.ViewData["title"] = product.Title;
            this.ViewData["gameDescription"] = product.Description;
            this.ViewData["price"] = product.Price.ToString("F2");
            this.ViewData["thumbnail"] = product.Thumbnail;
            this.ViewData["trailer"] = $"https://www.youtube.com/embed/{product.Trailer}?autoplay=1";
            this.ViewData["size"] = product.Size.ToString("F1");
            this.ViewData["release-date"] = string.IsNullOrWhiteSpace(product.ReleaseDate.ToString())? "N/A" : product.ReleaseDate.ToString();

            var buttons = new StringBuilder();

            string backButton = $@"<a class=""btn btn-outline-primary"" href=""{HomePath}"">Back</a>";

            buttons.AppendLine(backButton);

            if (Authentication.IsAdmin)
            {
                string editButton = $@"<a class=""btn btn-warning"" href=""/admin/games/edit/{product.Id}"">Edit</a>";
                string deleteButton = $@"<a class=""btn btn-danger"" href=""/admin/games/delete/{product.Id}"">Delete</a>";

                buttons.AppendLine(editButton);
                buttons.AppendLine(deleteButton);
            }

            string buyButton = $@"<a class=""btn btn-primary"" href=""/games/cart"">Buy</a>";

            buttons.AppendLine(buyButton);

            this.ViewData["buttons"] = buttons.ToString().TrimEnd();

            return this.FileViewResponse(@"games\game-details");
        }

        public IHttpResponse Buy(IHttpRequest request, int id)
        {
            var shoppingCart = request.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);

            shoppingCart.ProductIds.Add(id);

            return this.FileViewResponse(@"games\cart");
        }

        public IHttpResponse ShowCart(IHttpRequest request)
        {
            var shoppingCart = request.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);
            if (shoppingCart.ProductIds.Any())
            {
                this.ViewData["cartItems"] = "No items in your cart";
                this.ViewData["totalCost"] = "0.00";
            }
            else
            {
                var productsInCart = this.games
                    .FindProductsInCart(shoppingCart.ProductIds);
                

                var totalPrice = productsInCart
                    .Sum(pr => pr.Price);

                var productsHtml = productsInCart.Select(g => g.ToHtml());

                this.ViewData["games"] = string.Join(string.Empty, productsHtml);
                this.ViewData["total-price"] = totalPrice.ToString();
            }

            return this.FileViewResponse("games/cart");
        }

        public IHttpResponse Finish(IHttpRequest request)
        {
            var shoppingCart = request.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);
            shoppingCart.Clear();

            return this.RedirectResponse(HomePath);
        }
    }
}
