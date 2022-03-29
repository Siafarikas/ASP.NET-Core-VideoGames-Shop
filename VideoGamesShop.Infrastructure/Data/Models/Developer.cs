using System.ComponentModel.DataAnnotations;

namespace VideoGamesShop.Infrastructure.Data.Models
{
    public class Developer
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public IList<Game> Games { get; set; } = new List<Game>();
    }
}