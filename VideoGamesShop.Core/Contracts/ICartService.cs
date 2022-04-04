using VideoGamesShop.Core.Models.Cart;

namespace VideoGamesShop.Core.Contracts
{
    public interface ICartService
    {
        Task<IEnumerable<CartItemViewModel>> UsersCart(string userId);

        Task<bool> AddToCart(string userId, string productId);

        Task<bool> RemoveFromCart(string productId, string user);
    }
}
