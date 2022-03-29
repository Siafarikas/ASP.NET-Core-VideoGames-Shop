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
            return await repo.All<Game>()
                .Select(g => new GameListViewModel()
                {
                    Id = g.Id.ToString(),
                    Title = g.Title,
                    Price = g.Price,
                    Genre = g.Genre.ToString(),
                    ImageUrl = g.ImageUrl
                })
                .ToListAsync();
        }
    }
}
