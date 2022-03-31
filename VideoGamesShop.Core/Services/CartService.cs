using Microsoft.EntityFrameworkCore;
using VideoGamesShop.Core.Contracts;
using VideoGamesShop.Core.Models.Cart;
using VideoGamesShop.Infrastructure.Data.Identity;
using VideoGamesShop.Infrastructure.Data.Models;
using VideoGamesShop.Infrastructure.Data.Repositories;

namespace VideoGamesShop.Core.Services
{
    public class CartService : ICartService
    {
        private readonly IApplicatioDbRepo repo;
        private readonly IUserService userService;

        public CartService(
            IApplicatioDbRepo _repo,
            IUserService _userService)
        {
            repo = _repo;
            userService = _userService;
        }

        public async Task<bool> AddToCart(string userId, string gameId)
        {
            var user = await userService.GetUserById(userId);

            if (user == null) return false;


            var product = await repo.GetByIdAsync<Game>(gameId);

            if (product == null) return false;



            bool itemInCart = await repo.All<Item>()
                .Where(i => i.UserId == userId)
                .AnyAsync(i => i.GameId == gameId);

            if (itemInCart)
            {
                return false;
            }

            var cartItem = new Item()
            {
                UserId = userId,
                GameId = gameId
            };

            await repo.AddAsync(cartItem);
            await repo.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(string productId, string user)
        {
            throw new NotImplementedException();
        }


        public async Task<IEnumerable<CartItemViewModel>> UsersCart(string userId)
        {
            return await (from i in repo.All<Item>()
                          from g in repo.All<Game>().Where(g => g.Id == i.GameId).DefaultIfEmpty()
                          select new CartItemViewModel()
                          {
                              GameId = i.GameId,
                              Title = g.Title,
                              Price = g.Price,
                              ImageUrl = g.ImageUrl

                          }).ToListAsync();
        }

    }


}
