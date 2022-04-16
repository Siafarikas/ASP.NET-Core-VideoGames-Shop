namespace VideoGamesShop.Core.Models.Wishlist
{
    public class WishListViewModel
    {
        public string UserId { get; set; }

        public string GameId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Genre { get; set; }

        public string Developer { get; set; }

        public string ReleaseDate { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }
    }
}
