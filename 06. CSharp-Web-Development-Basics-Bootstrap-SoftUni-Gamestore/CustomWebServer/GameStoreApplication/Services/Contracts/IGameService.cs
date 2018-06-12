namespace CustomWebServer.GameStoreApplication.Services.Contracts
{
    using ViewModels.Admin;
    using System;
    using System.Collections.Generic;

    public interface IGameService
    {
        void Create(string title,  string description,
            string thumbnail, decimal price,
            double size, string trailer, 
            DateTime? releaseDate);

        IEnumerable<AdminListGameViewModel> AllGames();

        AdminEditGameViewModel FindById(int id);

        void Edit(AdminEditGameViewModel model);

        void DeleteById(int id);

        ICollection<string> AnonymousGamePaths();

        IEnumerable<AdminListGameViewModel> FindProductsInCart(IEnumerable<int> ids);
    }
}
