using System.ComponentModel.DataAnnotations;

namespace VideoGamesShop.Infrastructure.Data.Models
{
    public class Developer
    {
        [Key]
        public string Id { get; set; } 

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public IList<Game> PublishedGames { get; init; } = new List<Game>();

        public string UserId { get; set; }
    }
}