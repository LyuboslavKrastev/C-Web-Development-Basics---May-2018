namespace CustomWebServer.GameStoreApplication.Services
{
    using System;
    using System.Collections.Generic;
    using Contracts;
    using ViewModels.Admin;
    using Data;
    using Data.Models;
    using System.Linq;

    public class GameService : IGameService
    {
        public IEnumerable<AdminListGameViewModel> AllGames()
        {
            using (var db = new GameStoreDbContext())
            {
                var allGames = db.Games
                    .Select(g => new AdminListGameViewModel
                    {
                        Id = g.Id,
                        Name = g.Title,
                        Price = g.Price,
                        Thumbnail = g.ThumbnailUrl,
                        Description = g.Description,
                        Size = g.Size
                    });

                return allGames.ToList();
            }
        }

        public void Create(string title, string description, string thumbnail,
            decimal price, double size, string trailer, DateTime? releaseDate)
        {
            using (var db = new GameStoreDbContext())
            {
                var game = new Game
                {
                    Title = title,
                    Description = description,
                    Price = price,
                    Size = size,
                    ThumbnailUrl = thumbnail,
                    Trailer = trailer,
                    ReleaseDate = releaseDate
                };

                db.Games.Add(game);
                db.SaveChanges();
            }
        }

        public AdminEditGameViewModel FindById(int id)
        {
            using (var db = new GameStoreDbContext())
            {
                return db
                    .Games
                    .Where(g => g.Id == id)
                    .Select(g => new AdminEditGameViewModel
                    {
                        Id = g.Id,
                        Description = g.Description,
                        Size = g.Size,
                        Price = g.Price,
                        ReleaseDate = g.ReleaseDate,
                        Trailer = g.Trailer,
                        Thumbnail = g.ThumbnailUrl,
                        Title = g.Title
                    })
                    .FirstOrDefault();
            }
        }

        public void Edit(AdminEditGameViewModel model)
        {
            using (var db = new GameStoreDbContext())
            {
                var game = db.Games
                    .FirstOrDefault(g => g.Id == model.Id);

                if (game == null)
                {
                    return;
                }

                game.Description = model.Description;
                game.ThumbnailUrl = model.Thumbnail;
                game.Price = model.Price;
                game.ReleaseDate = model.ReleaseDate;
                game.Size = model.Size;
                game.Title = model.Title;
                game.Trailer = model.Trailer;

                db.SaveChanges();
            }
        }

        public void DeleteById(int id)
        {
            using (var db = new GameStoreDbContext())
            {
                var game = db.Games.FirstOrDefault(g => g.Id == id);

                if (game != null)
                {
                    db.Games.Remove(game);
                }

                db.SaveChanges();
            }
        }

        public ICollection<string> AnonymousGamePaths()
        {
            using (var db = new GameStoreDbContext())
            {
                if (db.Games.Any())
                {
                    var result = db
                        .Games
                        .Select(g => $@"/games/{g.Id}")
                        .ToList();

                    return result;
                }

                return null;
            }
        }

        public IEnumerable<AdminListGameViewModel> FindProductsInCart(IEnumerable<int> ids)
        {
            using (var db = new GameStoreDbContext())
            {
                return db.Games
                    .Where(pr => ids.Contains(pr.Id))
                    .Select(pr => new AdminListGameViewModel
                    {
                        Name = pr.Title,
                        Price = pr.Price,
                        Description = pr.Description,
                        Size = pr.Size,
                        Thumbnail = pr.ThumbnailUrl,

                    })
                    .ToList();
            }
        }
    }
}
