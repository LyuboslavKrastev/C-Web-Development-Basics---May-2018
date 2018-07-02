using ChushkaApp.App.HtmlHelpers;
using ChushkaApp.Data;
using ChushkaApp.Models;
using SoftUni.WebServer.Mvc.Controllers;
using SoftUni.WebServer.Mvc.Interfaces;
using System;
using System.Linq;
using System.Text;

namespace ChushkaApp.App.Controllers
{
    public class BaseController : Controller
    {
        private const string ErrorBox = @"<div class=""alert alert-danger text-center"">
                                        <strong>{0}</strong>
                                    </div>";

        protected BaseController()
        {
            this.Context = new CshushkaAppContext();
            this.ViewData["error"] = string.Empty;
        }

        protected CshushkaAppContext Context { get; private set; }

        protected User DbUser { get; private set; }

        protected IActionResult RedirectToHome()
        {
            return this.RedirectToAction("/home/index");
        }

        protected virtual void BuildErrorView()
        {
            var errors = this.ParameterValidator.ModelErrors.Values.ToList();
            var errorResult = new StringBuilder();

            foreach (var error in errors)
            {
                errorResult.AppendLine(string.Join(Environment.NewLine, error));
            }
            this.ViewData["error"] = string.Format(ErrorBox, errorResult.ToString());
        }

        public string UserRole => this.User.Roles.First().ToLower();

        public bool IsAdmin => this.UserRole == "admin";

        public override void OnAuthentication()
        {
            if (!this.User.IsAuthenticated)
            {
                this.ViewData["topMenu"] = UnauthenticatedIndexString.TopMenuHtml;
            }
            else if (this.UserRole == "admin")
            {
                this.ViewData["topMenu"] = AdminIndexString.TopMenuHtml;
            }
            else if (this.User.IsAuthenticated)
            {
                this.ViewData["topMenu"] = AuthenticatedIndexString.TopMenuHtml;
            }
         

            if (this.User.IsAuthenticated)
            {
                this.DbUser = this.Context.Users.FirstOrDefault(u => u.Username == this.User.Name);
            }

            base.OnAuthentication();
        }
    }
}
