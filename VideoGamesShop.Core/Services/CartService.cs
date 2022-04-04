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

        public async Task<bool> RemoveFromCart(string userId, string gameId)
        {
            var product = repo.All<Item>().Where(p => p.GameId == gameId && p.UserId == userId).FirstOrDefault();
            if (product == null)
            {
                return false;
            }

            repo.Delete(product);
            repo.SaveChanges();
            return true;
        }


        public async Task<IEnumerable<CartItemViewModel>> UsersCart(string userId)
        {
            return await (from i in repo.All<Item>()
                          from g in repo.All<Game>().Where(g => g.Id == i.GameId).DefaultIfEmpty()
                          from d in repo.All<Developer>().Where(d => d.Id == g.DeveloperId).DefaultIfEmpty()
                          from gr in repo.All<Genre>().Where(gr=> gr.Id == g.GenreId).DefaultIfEmpty()
                          select new CartItemViewModel()
                          {
                              GameId = i.GameId,
                              Title = g.Title,
                              Price = g.Price,
                              ImageUrl = g.ImageUrl,
                              Description = g.Description,
                              Developer = d.Id,
                              Genre = gr.Title,
                              ReleaseDate = g.ReleaseDate.ToString("yyyy-MM-dd")

                          }).ToListAsync();
        }


        //public async Task<IEnumerable<CartItemViewModel>> UsersCart(string userId)
        //{
        //    return await (from i in repo.All<Item>()
        //                  from g in repo.All<Game>().Where(g => g.Id == i.GameId).DefaultIfEmpty()
        //                  select new CartItemViewModel()
        //                  {
        //                      GameId = i.GameId,
        //                      Title = g.Title,
        //                      Price = g.Price,
        //                      ImageUrl = g.ImageUrl,
        //                      Description = g.Description,
        //                      Developer = g.Developer.ToString(),
        //                      Genre = g.Genre.ToString()

        //                  }).ToListAsync();
        //}
    }


}
