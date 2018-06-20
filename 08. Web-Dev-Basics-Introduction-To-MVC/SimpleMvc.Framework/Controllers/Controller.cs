namespace SimpleMvc.Framework.Controllers
{
    using Contracts;
    using Contracts.Generic;  
    using ViewEngine;
    using System.Runtime.CompilerServices;
    using Helpers;
    using ViewEngine.Generic;

    public abstract class Controller
    {
        //instantiates <assembly>.Views.Home.Index.cs class
        protected IActionResult View([CallerMemberName]string caller = "")
        {
            var controllerName = ControllerHelpers
                .GetControllerName(this);

            var viewFullQualifiedName = ControllerHelpers
                .GetViewFullQualifiedName(controllerName, caller);

            return new ActionResult(viewFullQualifiedName);
        }

        //might be used when we want to redirect the user to different page
        protected IActionResult View(string controller, string action)
        {
             var viewFullQualifiedName = ControllerHelpers
                .GetViewFullQualifiedName(controller, action);

            return new ActionResult(viewFullQualifiedName);
        }

        //same as the View() method but also a model is provided to the view so dynamic content can be created
        protected IActionResult<TModel> View<TModel>(TModel model, [CallerMemberName]string caller = "")
        {
             var controllerName = ControllerHelpers
                .GetControllerName(this);

            var viewFullQualifiedName = ControllerHelpers
                .GetViewFullQualifiedName(controllerName, caller);

                return new ActionResult<TModel>(viewFullQualifiedName, model);
        }

        //same as View(controller, action) but also a model is provided to the view so dynamic content can be created.
        protected IActionResult<TModel> View<TModel>(
            TModel model, string controller, string action)
        {
            var viewFullQualifiedName = ControllerHelpers
                .GetViewFullQualifiedName(controller, action);

                return new ActionResult<TModel>(viewFullQualifiedName, model);
        }
    }
}
