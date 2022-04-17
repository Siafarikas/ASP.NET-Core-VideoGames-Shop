using VideoGamesShop.Core.Models.Wishlist;

namespace VideoGamesShop.Core.Contracts
{
    public interface IWishlistService
    {
        Task<IEnumerable<WishListViewModel>> UsersWishlist(string userId);

        Task<bool> AddToWishlist(string userId, string productId);

        Task<bool> RemoveFromWishlist(string userId, string gameId);

        Task<bool> MoveToCart(string userId, string gameId);
    }
}
