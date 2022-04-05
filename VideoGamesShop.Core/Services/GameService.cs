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
                              Developer = $"{dev.FirstName} {dev.LastName}"
                          }).ToListAsync();
        }
    }
}
