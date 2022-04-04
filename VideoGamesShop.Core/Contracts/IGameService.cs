using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGamesShop.Core.Models;
using VideoGamesShop.Core.Models.Game;

namespace VideoGamesShop.Core.Contracts
{
    public interface IGameService
    {
        Task<IEnumerable<GameListViewModel>> GetGames();

        Task<GameDetailsViewModel> GameDetails(string gameId);
    }
}
