namespace  SimpleMvc.Framework.Routers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using WebServer.Contracts;
    using WebServer.Exceptions;
    using System.Reflection;
    using WebServer.Http.Response;
    using Helpers; 
    using Attributes.Methods;
    using WebServer.Http.Contracts;
    using Framework.Controllers;
    using Contracts;
    using WebServer.Enums;

    public class ControllerRouter : IHandleable
    {
        private IDictionary<string, string> getParameters;
        private IDictionary<string, string> postParameters;
        private string requestMethod;
        private string controllerName;
        private string actionName;
        private object[] methodParameters;

        public IHttpResponse Handle(IHttpRequest request)
        {
            this.getParameters = new Dictionary<string, string>(request.UrlParameters);
            this.postParameters = new Dictionary<string, string>(request.FormData);
            this.requestMethod = request.Method.ToString().ToUpper();

            this.PrepareControllerAndActionNames(request);

            //<host>/{controllerName}/{actionName}?{query_string}
            var method = this.GetMethod();

            if (method == null)
            {
                return new NotFoundResponse();
            }

            this.PrepareMethodParameters(method);

            IHttpResponse response = this.PrepareResponse(method);

            return response;
        }

        private IHttpResponse PrepareResponse(MethodInfo method)
        {
            var actionResult = (IInvocable)method.Invoke(this.GetController(), this.methodParameters);

            var content = actionResult.Invoke();

            IHttpResponse response = new ContentResponse(HttpStatusCode.Ok, content);

            return response;
        }

        private void PrepareControllerAndActionNames(IHttpRequest request)
        {
            var pathParts = request.Path.Split(
                new[] { '/', '?' },
                StringSplitOptions.RemoveEmptyEntries);

            if (pathParts.Length < 2)
            {
                BadRequestException.ThrowFromInvalidRequest();
            }

            this.controllerName = $"{pathParts[0].Capitalize()}{MvcContext.Get.ControllerSuffix}";
            this.actionName = pathParts[1].Capitalize();
        }


        /* Because of the overloading of methods in one controller class there might be several methods with same name so we need to obtain all of them. 
       Then we need to iterate over every one of them and check if they are annotated with some HttpMethodAttribute. 
       If the method is not annotated with any HttpMethodAttribute and the request method is GET we should return it. 
       Otherwise we check if the attribute on the method is the same as the request’s method. 
       If the attribute of the method and the requested method are the same => that’s our method and we should return it. */

        private MethodInfo GetMethod()
        {
            foreach (var methodInfo in this.GetSuitableMethods())
            {
                IEnumerable<Attribute> httpMethodAttributes = methodInfo
                    .GetCustomAttributes()
                    .Where(a => a is HttpMethodAttribute)
                    .Cast<HttpMethodAttribute>();

                if (!httpMethodAttributes.Any() && this.requestMethod == "GET")
                {
                    return methodInfo;
                }

                foreach (HttpMethodAttribute httpMethodAttribute in httpMethodAttributes)
                {
                    if (httpMethodAttribute.IsValid(this.requestMethod))
                    {
                        return methodInfo;
                    }
                }
            }

            return null;
        }

        //gets all methods of the requested controller.
        private IEnumerable<MethodInfo> GetSuitableMethods()
        {
            var controller = this.GetController();

            if (controller == null)
            {
                return new MethodInfo[0];
            }

            return controller
                .GetType()
                .GetMethods()
                .Where(m => m.Name.ToLower() == actionName.ToLower());
        }

        //creates an instance of the requested controller using the full path to the controller in the project
        private object GetController()
        {
            var controllerFullqualifiedName = string
                .Format("{0}.{1}.{2}, {0}",
                MvcContext.Get.AssemblyName,
                MvcContext.Get.ControllersFolder,
                this.controllerName);

            var controllerType = Type.GetType(controllerFullqualifiedName);

            if(controllerType == null)
            {
                return null;
            }

            return (Controller)Activator.CreateInstance(controllerType);
        }

        /*When we obtain the method of the controller we need to convert the string parameters 
        from getParams or postParams dictionaries to their appropriate type that the method in the controller expects.*/
        private void PrepareMethodParameters(MethodInfo methodInfo)
        {
            var parameters = methodInfo.GetParameters();

            this.methodParameters = new object[parameters.Length];

            for (var i = 0; i < parameters.Length; i++)
            {
                var parameter = parameters[i];

                if (parameter.ParameterType.IsPrimitive
                    || parameter.ParameterType == typeof(string))
                {
                    if (!this.getParameters.ContainsKey(parameter.Name))
                    {
                        continue;
                    }

                    var getParametersValue = this.getParameters[parameter.Name];

                    var value = Convert.ChangeType(getParametersValue, parameter.ParameterType);

                    this.methodParameters[i] = value;
                }
                else
                {
                    var bindingModelType = parameter.ParameterType;
                    var bindingModel = Activator.CreateInstance(bindingModelType);

                    var modelProperties = bindingModelType.GetProperties();

                    foreach (var modelProperty in modelProperties)
                    {
                        var postParameterValue = this.postParameters[modelProperty.Name];
                        var value = Convert.ChangeType(postParameterValue, modelProperty.PropertyType);

                        modelProperty.SetValue(bindingModel, value);
                    }
                    this.methodParameters[i] = Convert.ChangeType(
                      bindingModel, bindingModelType);
                }
            }
       }
    }
}