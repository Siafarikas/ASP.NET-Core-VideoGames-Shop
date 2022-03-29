using System.ComponentModel.DataAnnotations;

namespace VideoGamesShop.Infrastructure.Data.Models
{
    public class Item
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string GameId { get; set; }

        public Game Game { get; set; }
    }
}