using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGamesShop.Core.Contracts;
using VideoGamesShop.Core.Models;
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
