using VideoGamesShop.Infrastructure.Data.Identity;

namespace VideoGamesShop.Infrastructure.Data.Models
{
    public class Wish
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public string GameId { get; set; }
        public Game Game { get; set; }
    }
}
