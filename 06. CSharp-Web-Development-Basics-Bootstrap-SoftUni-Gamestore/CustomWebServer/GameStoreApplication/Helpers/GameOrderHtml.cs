namespace CustomWebServer.GameStoreApplication.Helpers
{
    using ViewModels.Admin;
    public static class GameOrderHtml
    {
        public static string ToHtml(this AdminListGameViewModel game)
           => $@"
                <div class=""list-group-item"">
                    <div class=""media"">
                        <a class=""btn btn-outline-danger btn-lg align-self-center mr-3"" href=""/orders/remove?id={game.Id}"">X</a>
                        <img class=""d-flex mr-4 align-self-center img-thumbnail"" height=""127"" src=""{game.Thumbnail}"" onerror=""https://i.ytimg.com/vi/{game.Trailer}/maxresdefault.jpg""
                                width=""227"" alt=""{game.Name}"">
                        <div class=""media-body align-self-center"">
                            <a href=""/games/details?id={game.Id}"">
                                <h4 class=""mb-1 list-group-item-heading"">{game.Name}</h4>
                            </a>
                            <p>
                                {game.Description}
                            </p>
                        </div>
                        <div class=""col-md-2 text-center align-self-center mr-auto"">
                            <h2>{game.Price}&euro;</h2>
                        </div>
                    </div>
                </div>";
    }
}
