using VideoGamesShop.Core.Models;
using VideoGamesShop.Core.Models.Game;
using VideoGamesShop.Core.Models.Wishlist;

namespace VideoGamesShop.Core.Contracts
{
    public interface IGameService
    {
        Task<IEnumerable<GameListViewModel>> GetGames();

        Task<GameDetailsViewModel> GameDetails(string gameId);

        Task<IEnumerable<GameLibraryViewModel>> GetUsersGames(string userId);


        Task<bool> AddGame(string title, string genreId, decimal price, string releaseDate, string description, string imageUrl, string developerId);

        Task<IEnumerable<GameGenreModel>> GetAllGenres();

        Task<bool> GameWithIdExists(string gameId);

        Task<string> GetGenreTitleById(string genreId);
    }
}
