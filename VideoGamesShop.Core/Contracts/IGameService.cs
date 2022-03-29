using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGamesShop.Core.Models;

namespace VideoGamesShop.Core.Contracts
{
    public interface IGameService
    {
        Task<IEnumerable<GameListViewModel>> GetGames();
    }
}
