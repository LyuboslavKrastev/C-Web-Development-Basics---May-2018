namespace SimpleMvc.App.Controllers
{
    using Framework.Controllers;
    using Framework.Contracts;
    using Framework.Attributes.Methods;

    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
             return View();
        }

        //[HttpPost]
        //public IActionResult Index(IndexViewModel model)
        //{
        //     return View();
        //}
    }
}
