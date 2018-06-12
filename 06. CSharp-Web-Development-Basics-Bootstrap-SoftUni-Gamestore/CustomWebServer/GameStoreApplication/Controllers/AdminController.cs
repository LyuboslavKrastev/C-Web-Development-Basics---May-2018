namespace CustomWebServer.GameStoreApplication.Controllers
{
    using ViewModels.Admin;
    using Server.Http.Contracts;
    using Services.Contracts;
    using Services;
    using System.Linq;
    using System;

    public class AdminController : BaseController
    {
        private const string AddGameView = @"admin\add-game";
        private const string ListGamesView = @"admin\list-games";


        private readonly IGameService games;

        public AdminController(IHttpRequest request) 
            : base(request)
        {
            this.games = new GameService();
        }

        public IHttpResponse Add()
        {
            if (!this.Authentication.IsAdmin)
            {
                return this.RedirectResponse(HomePath);      
            }

            return this.FileViewResponse(AddGameView);
        }

        public IHttpResponse Add(AdminAddGameViewModel model)
        {
            if (!this.Authentication.IsAdmin)
            {
                return this.RedirectResponse(HomePath);
            }

            if (!this.ValidateModel(model))
            {
                return this.Add();
            }

            this.games.Create(
                model.Title, model.Description, model.ThumbnailUrl, 
                model.Price,model.Size, model.Trailer, model.ReleaseDate);

            return this.RedirectResponse("/admin/games/list");
        }

        public IHttpResponse List()
        {
            if (!this.Authentication.IsAdmin)
            {
                return this.RedirectResponse(HomePath);
            }

            var result = this.games
                .AllGames()
                .Select(g => $@"<tr>
                                    <td>{g.Id}</td>
                                    <td>{g.Name}</td>
                                    <td>{g.Size:F2} GB</td>
                                    <td>{g.Price:F2} &euro;</td>
                                    <td>
                                        <a class=""btn btn-warning"" href=""/admin/games/edit/{g.Id}"">Edit</a>
                                        <a class=""btn btn-danger"" href=""/admin/games/delete/{g.Id}"">Delete</a>
                                    </td>
                                </tr>");

            var gamesHtml = string.Join(Environment.NewLine, result);

            this.ViewData["games"] = gamesHtml;

            return this.FileViewResponse(ListGamesView);
        }

        public IHttpResponse Edit()
        {
            var id = int.Parse(this.Request.UrlParameters["id"]);

            var model = this.games.FindById(id);

            this.ViewData["title"] = model.Title;
            this.ViewData["description"] = model.Description;
            this.ViewData["thumbnail"] = model.Thumbnail;
            this.ViewData["price"] = model.Price.ToString("f2");
            this.ViewData["size"] = model.Size.ToString("f1");
            this.ViewData["trailer"] = model.Trailer;
            this.ViewData["release-date"] = model.ReleaseDate.ToString();

            return this.FileViewResponse(@"admin\edit-game");
        }


        public IHttpResponse Edit(AdminEditGameViewModel model)
        {
            if (!this.ValidateModel(model))
            {
                return this.Edit();
            }

            this.games.Edit(model);

            return this.List();
        }

        public IHttpResponse Delete(int id)
        {
            var model = this.games.FindById(id);

            this.ViewData["title"] = model.Title;
            this.ViewData["description"] = model.Description;
            this.ViewData["thumbnail"] = model.Thumbnail;
            this.ViewData["price"] = model.Price.ToString("f2");
            this.ViewData["size"] = model.Size.ToString("f1");
            this.ViewData["trailer"] = model.Trailer;
            this.ViewData["release-date"] = string.IsNullOrWhiteSpace(model.ReleaseDate.ToString())? 
                "No Release Date Provided" : model.ReleaseDate.ToString();

            return this.FileViewResponse(@"admin\delete-game");
        }

        public IHttpResponse Delete()
        {
            var id = int.Parse(this.Request.UrlParameters["id"]);

            this.games.DeleteById(id);

            return this.List();
        }
    }
}
