using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGamesShop.Core.Contracts;
using VideoGamesShop.Core.Models;
using VideoGamesShop.Core.Models.Game;
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
#pragma warning disable CS8603 // Possible null reference return.

            return await (from game in repo.All<Game>()
                          .Where(g => g.Id == gameId)
                          from genre in repo.All<Genre>().Where(gr => gr.Id == game.GenreId).DefaultIfEmpty()
                          select new GameDetailsViewModel()
                          {
                              GameId = game.Id.ToString(),
                              Description = game.Description,
                              ReleaseDate = game.ReleaseDate.ToString("yyyy-MM-dd"),
                              Developer = game.Developer.Id.ToString(),
                              Title = game.Title,
                              Price = game.Price,
                              Genre = genre.Title,
                              ImageUrl = game.ImageUrl
                          })
                          .FirstOrDefaultAsync();

#pragma warning restore CS8603 // Possible null reference return.

        }

        public async Task<IEnumerable<GameListViewModel>> GetGames()
        {
            return await (from game in repo.All<Game>()
                          from genre in repo.All<Genre>().Where(gr => gr.Id == game.GenreId).DefaultIfEmpty()
                          select new GameListViewModel()
                          {
                              Id = game.Id.ToString(),
                              Title = game.Title,
                              Price = game.Price,
                              Genre = genre.Title,
                              ImageUrl = game.ImageUrl
                          }).ToListAsync();
        }
    }
}
