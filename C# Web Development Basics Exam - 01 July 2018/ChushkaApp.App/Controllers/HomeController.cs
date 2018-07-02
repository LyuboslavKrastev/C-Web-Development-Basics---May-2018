using ChushkaApp.App.HtmlHelpers;
using SoftUni.WebServer.Mvc.Attributes.HttpMethods;
using SoftUni.WebServer.Mvc.Interfaces;
using System;
using System.Linq;
using System.Text;

namespace ChushkaApp.App.Controllers
{
    public class HomeController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            if (!this.User.IsAuthenticated)
            {
                this.ViewData["result"] = UnauthenticatedIndexString.IndexHtml;
            }
            else
            {
                var result = new StringBuilder();
                result.Append($@"<div class=""container-fluid text-center"">
                        <h2>Greetings, {User.Name}!</h2>
                        <h4>Feel free to view and order any of our products.</h4>
                    </div>
                    <hr class=""hr-2 bg-dark""/>");

                result.Append(@"<div class=""container-fluid product-holder"">
                         <div class=""row d-flex justify-content-around"">");

                var products = Context.Products.ToList();

                for (int i = 0; i < products.Count; i++)
                {
                    var product = products[i];

                    var currentProductHtml = $@"<a href=""/products/details?id={product.Id}"" class=""col-md-2"">
                <div class=""product p-1 chushka-bg-color rounded-top rounded-bottom"">
                    <h5 class=""text-center mt-3"">{product.Name}</h5>
                    <hr class=""hr-1 bg-white""/>
                    <p class=""text-white text-center"">
                        {product.Description}
                    </p>
                    <hr class=""hr-1 bg-white""/>
                    <h6 class=""text-center text-white mb-3"">${product.Price}</h6>
                </div>
                </a>";

                    result.Append(currentProductHtml);
                }
                result.Append("</div>");
                result.Append("</div>");

                this.ViewData["result"] = result.ToString();
            }

            return this.View();
        }
    }
}
