using SoftUni.WebServer.Mvc.Attributes.HttpMethods;
using SoftUni.WebServer.Mvc.Attributes.Security;
using SoftUni.WebServer.Mvc.Interfaces;
using System.Linq;
using System.Text;

namespace ChushkaApp.App.Controllers
{
    public class OrdersController : BaseController
    {
        [HttpGet]
        [Authorize]
        public IActionResult All()
        {
            if (!this.IsAdmin)
            {
                return RedirectToHome();
            }

            var orders = this.Context.Orders.ToList();
            var result = new StringBuilder();


            for (int i = 0; i < orders.Count; i++)
            {
                var order = orders[i];
                var clientId = order.ClientId;
                var clientName = Context.Users.FirstOrDefault(c => c.Id == clientId).Username;

                var productId = order.ProductId;
                var productName = Context.Products.FirstOrDefault(p => p.Id == productId).Name;

                var orderHtml = $@"
                <tr>
                <th scope=""row"">{i}</th>
                <td>{order.Id}</td>
                <td>{clientName}</td>
                <td>{productName}</td>
                <td>{order.OrderedOn}</td>
              </tr>";

                result.Append(orderHtml);
            }

            this.ViewData["result"] = result.ToString();

            return this.View();
        }
    }
}
