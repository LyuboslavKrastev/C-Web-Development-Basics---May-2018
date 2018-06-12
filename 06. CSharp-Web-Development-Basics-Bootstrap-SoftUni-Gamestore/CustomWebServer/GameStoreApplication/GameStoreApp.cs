namespace CustomWebServer.GameStoreApplication
{
    using Server.Routing.Contracts;
    using Server.Contracts;
    using GameStoreApplication.Data;
    using Microsoft.EntityFrameworkCore;
    using GameStoreApplication.Controllers;
    using ViewModels.Account;
    using ViewModels.Admin;
    using System;
    using System.Globalization;
    using Services.Contracts;
    using Services;

    public class GameStoreApp : IApplication
    {
        private readonly IGameService games = new GameService();

        public void InitializeDatabase()
        {
            using (var db = new GameStoreDbContext())
            {
                db.Database.Migrate();
            }
        }

        public void Configure(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig
                   .AnonymousPaths.Add("/");

            appRouteConfig
                .AnonymousPaths.Add("/account/register");

            appRouteConfig
                .AnonymousPaths.Add("/account/login");

            var anonymousGamePaths = this.games.AnonymousGamePaths();
            if (anonymousGamePaths != null)
            {
                foreach (var gamePath in anonymousGamePaths)
                {
                    appRouteConfig.AnonymousPaths.Add(gamePath);
                }
            }

            appRouteConfig
                .Get("/account/register",
                req => new AccountController(req).Register());


            appRouteConfig
                .Post("/account/register",
                req => new AccountController(req)
                .Register(new RegisterViewModel
                {
                    Email = req.FormData["email"],
                    FullName = req.FormData["full-name"],
                    Password = req.FormData["password"],
                    ConfirmPassword = req.FormData["confirm-password"]
                }));

            appRouteConfig
                .Get("/account/login",
                req => new AccountController(req).Login());

            appRouteConfig
                .Post("/account/login",
                req => new AccountController(req)
                .Login(
                new LoginViewModel
                {
                    Email = req.FormData["email"],
                    Password = req.FormData["password"]
                }));

            appRouteConfig
                .Get("/account/logout",
                req => new AccountController(req)
                .Logout(req));

            appRouteConfig
               .Get("/",
               req => new HomeController(req).Index());

          
            appRouteConfig
               .Get("/admin/games/add",
               req => new AdminController(req).Add());

            appRouteConfig
               .Post("/admin/games/add",
               req => new AdminController(req).Add(new AdminAddGameViewModel
               {
                   Title = req.FormData["title"],
                   Description = req.FormData["description"],
                   ThumbnailUrl = req.FormData["thumbnail"],
                   Price = string.IsNullOrWhiteSpace(req.FormData["price"]) ? -1 : decimal.Parse(req.FormData["price"]),
                   Size = string.IsNullOrWhiteSpace(req.FormData["size"]) ? -1 : double.Parse(req.FormData["size"]),
                   Trailer = req.FormData["trailer"],
                   ReleaseDate = string.IsNullOrEmpty(req.FormData["release-date"])? (DateTime?)null :
                   DateTime.ParseExact(
                       req.FormData["release-date"],  
                       "yyyy-MM-dd",
                        CultureInfo.InvariantCulture)             
               }
               ));

            appRouteConfig
                .Get("/admin/games/list",
                req => new AdminController(req).List());

            appRouteConfig
               .Get("/admin/games/edit/{(?<id>[0-9]+)}", req => new AdminController(req).Edit());

            appRouteConfig
               .Post("/admin/games/edit/{(?<id>[0-9]+)}",
                   req => new AdminController(req).Edit(
                       new AdminEditGameViewModel
                       {
                           Id = int.Parse(req.UrlParameters["id"]),
                           Title = req.FormData["title"],
                           Description = req.FormData["description"],
                           Thumbnail = req.FormData["thumbnail"],
                           Price = string.IsNullOrWhiteSpace(req.FormData["price"]) ? -1 : decimal.Parse(req.FormData["price"]),
                           Size = string.IsNullOrWhiteSpace(req.FormData["size"]) ? -1 : double.Parse(req.FormData["size"]),
                           Trailer = req.FormData["trailer"],
                           ReleaseDate = string.IsNullOrEmpty(req.FormData["release-date"]) ? (DateTime?)null :
                                DateTime.ParseExact(req.FormData["release-date"],
                                    "yyyy-MM-dd",
                                    CultureInfo.InvariantCulture)
                       }));

            appRouteConfig
             .Get("/admin/games/delete/{(?<id>[0-9]+)}",
                req => new AdminController(req).Delete(int.Parse(req.UrlParameters["id"])));

            appRouteConfig
                .Post("/admin/games/delete/{(?<id>[0-9]+)}",
                    req => new AdminController(req).Delete());

            appRouteConfig
                .Get("/games/{(?<id>[0-9]+)}",
                    req => new GameController(req).Details(int.Parse(req.UrlParameters["id"])));

            appRouteConfig
                .Post("/games/{(?<id>[0-9]+)}",
                req => new GameController(req).Buy(req, int.Parse(req.UrlParameters["id"])));


            appRouteConfig
                .Get("/games/cart",
                req => new GameController(req).ShowCart(req));


            appRouteConfig
                .Post("/games/cart",
                req => new GameController(req).Finish(req));
        }
    }
}
