using Microsoft.EntityFrameworkCore;
using System.Globalization;
using VideoGamesShop.Core.Contracts;
using VideoGamesShop.Core.Models.Cart;
using VideoGamesShop.Infrastructure.Data.Identity;
using VideoGamesShop.Infrastructure.Data.Models;
using VideoGamesShop.Infrastructure.Data.Repositories;

namespace VideoGamesShop.Core.Services
{
    public class CartService : ICartService
    {
        private readonly IApplicationDbRepo repo;
        private readonly IUserService userService;
        private readonly IWishlistService wishlistService;
         
        public CartService(
            IApplicationDbRepo _repo,
            IUserService _userService,
            IWishlistService _wishlistService)
        {
            repo = _repo;
            userService = _userService;
            wishlistService = _wishlistService;
        }

        public async Task<bool> AddToCart(string userId, string gameId)
        {
            var user = await userService.GetUserById(userId);

            if (user == null) return false;


            var product = await repo.GetByIdAsync<Game>(gameId);

            if (product == null) return false;

            var productOwned = await repo.All<Purchase>()
                .Where(p => p.UserId == user.Id)
                .AnyAsync(p => p.GameId == product.Id);

            if (productOwned) return false;


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

        public async Task<bool> RemoveFromCart(string userId, string gameId)
        {
            var product = await repo.All<Item>()
                .Where(p => p.GameId == gameId && p.UserId == userId)
                .FirstOrDefaultAsync();

            if (product == null)
            {
                return false;
            }

            repo.Delete(product);
            await repo.SaveChangesAsync();
            return true;
        }


        public async Task<IEnumerable<CartItemViewModel>> UsersCart(string userId)
        {
            var products = await repo.All<Item>().Where(i => i.UserId == userId).ToListAsync();

            return await (from i in repo.All<Item>().Where(i => i.UserId == userId)
                          from g in repo.All<Game>().Where(g => g.Id == i.GameId).DefaultIfEmpty()
                          from dev in repo.All<Developer>().Where(d => d.Id == g.DeveloperId).DefaultIfEmpty()
                          from gr in repo.All<Genre>().Where(gr => gr.Id == g.GenreId).DefaultIfEmpty()
                          select new CartItemViewModel()
                          {
                              GameId = i.GameId,
                              Title = g.Title,
                              Price = g.Price,
                              ImageUrl = g.ImageUrl,
                              Description = g.Description,
                              Developer = $"{dev.FirstName} {dev.LastName}",
                              Genre = gr.Title,
                              ReleaseDate = g.ReleaseDate.ToString("yyyy-MM-dd")

                          }).ToListAsync();
        }

        public async Task<bool> BuyProductsInCart(string userId)
        {
            List<Item> items = await (from i in repo.All<Item>().Where(i => i.UserId == userId)
                                      from g in repo.All<Game>().Where(g => g.Id == i.GameId).DefaultIfEmpty()
                                      select new Item()
                                      {
                                          UserId = i.UserId,
                                          GameId = i.GameId,
                                          Game = repo.All<Game>().Where(g => g.Id == i.GameId).SingleOrDefault()
                                      })
                           .ToListAsync();

            var user = await userService.GetUserById(userId);

            var totalAmount = items.Sum(g => g.Game.Price);

            if (user == null) return false;

            if (items.Count == 0)
            {
                return false;
            }

            if (user.Wallet < totalAmount)
            {
                return false;
            }

            foreach (var item in items)
            {
                Purchase purchase = new Purchase()
                {
                    UserId = item.UserId,
                    GameId = item.GameId
                };

                item.Game.Sales++;

                user.Games.Add(purchase);

                Wish wish = new Wish()
                {
                    UserId = item.UserId,
                    GameId = item.GameId
                };
                var gameInWishlist = repo.All<Wish>()
                    .Contains(wish);
                if (gameInWishlist)
                {
                    await wishlistService.RemoveFromWishlist(item.UserId, item.GameId);
                }
                repo.Delete(item);
            }

            user.Wallet -= totalAmount;
            await repo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> MoveToWishlist(string userId, string gameId)
        {
            var game = await repo.All<Item>()
                .Where(g => g.GameId == gameId && g.UserId == userId)
                .FirstOrDefaultAsync();

            if (game == null) return false;

            var wish = new Wish()
            {
                UserId = userId,
                GameId = gameId
            };

            bool gameInWishlist = await repo.All<Wish>()
                .ContainsAsync(wish);

            if (gameInWishlist)
            {
                return false;
            }

            await repo.AddAsync(wish);
            repo.Delete(game);
            await repo.SaveChangesAsync();
            return true;
        }

    }


}
