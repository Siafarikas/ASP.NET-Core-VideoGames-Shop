using Microsoft.EntityFrameworkCore;
using VideoGamesShop.Core.Contracts;
using VideoGamesShop.Core.Models.Wishlist;
using VideoGamesShop.Infrastructure.Data.Models;
using VideoGamesShop.Infrastructure.Data.Repositories;

namespace VideoGamesShop.Core.Services
{
    public class WishlistService : IWishlistService
    {
        private readonly IApplicationDbRepo repo;
        private readonly IUserService userService;

        public WishlistService(
            IApplicationDbRepo _repo,
            IUserService _userService)
        {
            repo = _repo;
            userService = _userService;
        }
        public async Task<IEnumerable<WishListViewModel>> UsersWishlist(string userId)
        {
            var products = await repo.All<Wish>().Where(i => i.UserId == userId).ToListAsync();

            return await (from w in repo.All<Wish>().Where(w => w.UserId == userId)
                          from g in repo.All<Game>().Where(g => g.Id == w.GameId).DefaultIfEmpty()
                          from dev in repo.All<Developer>().Where(d => d.Id == g.DeveloperId).DefaultIfEmpty()
                          from gr in repo.All<Genre>().Where(gr => gr.Id == g.GenreId).DefaultIfEmpty()
                          select new WishListViewModel()
                          {
                              GameId = w.GameId,
                              Title = g.Title,
                              Price = g.Price,
                              ImageUrl = g.ImageUrl,
                              Description = g.Description,
                              Developer = $"{dev.FirstName} {dev.LastName}",
                              Genre = gr.Title,
                              ReleaseDate = g.ReleaseDate.ToString("yyyy-MM-dd")

                          }).ToListAsync();
        }

        public async Task<bool> AddToWishlist(string userId, string gameId)
        {
            var user = await userService.GetUserById(userId);

            if (user == null) return false;


            var game = await repo.GetByIdAsync<Game>(gameId);

            if (game == null) return false;

            var gameOwned = await repo.All<Purchase>()
                 .Where(p => p.UserId == user.Id)
                 .AnyAsync(p => p.GameId == game.Id);

            if (gameOwned) return false;

            bool gameInWishlist = await repo.All<Wish>()
                .Where(i => i.UserId == userId)
                .AnyAsync(i => i.GameId == gameId);

            if (gameInWishlist)
            {
                return false;
            }

            var wish = new Wish()
            {
                UserId = userId,
                GameId = gameId
            };

            await repo.AddAsync(wish);
            await repo.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveFromWishlist(string userId, string gameId)
        {
            var game = await repo.All<Wish>()
                .Where(p => p.GameId == gameId && p.UserId == userId)
                .FirstOrDefaultAsync();

            if (game == null)
            {
                return false;
            }

            repo.Delete(game);
            await repo.SaveChangesAsync();
            return true;
        }


       
    }
}
