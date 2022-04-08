using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGamesShop.Core.Contracts;
using VideoGamesShop.Core.Models;
using VideoGamesShop.Core.Models.Game;
using VideoGamesShop.Infrastructure.Data.Identity;
using VideoGamesShop.Infrastructure.Data.Models;
using VideoGamesShop.Infrastructure.Data.Repositories;

namespace VideoGamesShop.Core.Services
{
    public class GameService : IGameService
    {
        private readonly IApplicatioDbRepo repo;

        public GameService(IApplicatioDbRepo _repo)
        {
            repo = _repo;
        }

        public async Task<GameDetailsViewModel> GameDetails(string gameId)
        {

            return await (from game in repo.All<Game>()
                          .Where(g => g.Id == gameId)
                          from genre in repo.All<Genre>().Where(gr => gr.Id == game.GenreId).DefaultIfEmpty()
                          from dev in repo.All<Developer>().Where(d => d.Id == game.DeveloperId).DefaultIfEmpty()
                          select new GameDetailsViewModel()
                          {
                              GameId = game.Id.ToString(),
                              Description = game.Description,
                              ReleaseDate = game.ReleaseDate.ToString("yyyy-MM-dd"),
                              Developer = $"{dev.FirstName} {dev.LastName}",
                              Title = game.Title,
                              Price = game.Price,
                              Genre = genre.Title,
                              ImageUrl = game.ImageUrl
                          })
                          .FirstOrDefaultAsync();


        }

        public async Task<IEnumerable<GameListViewModel>> GetGames()
        {
            return await (from game in repo.All<Game>()
                          from genre in repo.All<Genre>().Where(gr => gr.Id == game.GenreId).DefaultIfEmpty()
                          from dev in repo.All<Developer>().Where(d => d.Id == game.DeveloperId).DefaultIfEmpty()
                          select new GameListViewModel()
                          {
                              Id = game.Id.ToString(),
                              Title = game.Title,
                              Price = game.Price,
                              Genre = genre.Title,
                              ImageUrl = game.ImageUrl,
                          }).ToListAsync();
        }

        public async Task<IEnumerable<GameLibraryViewModel>> GetUsersGames(string userId)
        {
            return await (from user in repo.All<ApplicationUser>().Where(u => u.Id == userId).DefaultIfEmpty()
                          from purchase in repo.All<Purchase>().Where(p => p.UserId == user.Id).DefaultIfEmpty()
                          from game in repo.All<Game>().Where(game => game.Id == purchase.GameId).DefaultIfEmpty()
                          select new GameLibraryViewModel()
                          {
                              Id = game.Id,
                              Title = game.Title,
                              ImageUrl = game.ImageUrl,
                          }).ToListAsync();
        }


        public async Task<bool> AddGame(string title, string genreId, decimal price, string releaseDate, string description, string imageUrl, string developerId)
        {
            Genre genre = await repo.All<Genre>()
                .Where(g => g.Id == genreId)
                .FirstOrDefaultAsync();

            Developer dev = await repo.All<Developer>()
                .Where(d => d.Id == developerId)
                .FirstOrDefaultAsync();

            DateTime convertedDate;
            DateTime.TryParseExact(releaseDate, "yyyy-MM-dd", CultureInfo.InvariantCulture.DateTimeFormat, DateTimeStyles.None, out convertedDate);

            var game = new Game
            {
                Title = title,
                Genre = genre,
                Price = price,
                ReleaseDate = convertedDate,
                Description = description,
                ImageUrl = imageUrl,
                Developer = dev
                };

            await repo.AddAsync(game);
            await repo.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<GameGenreModel>> GetAllGenres()
        {
            return await repo.All<Genre>()
                .Select(g => new GameGenreModel()
                {
                    Id = g.Id,
                    Name = g.Title
                })
                .ToListAsync();
        }
    }
}
