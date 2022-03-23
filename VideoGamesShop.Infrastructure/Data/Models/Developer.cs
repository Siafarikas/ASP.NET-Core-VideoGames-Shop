namespace VideoGamesShop.Infrastructure.Data.Models
{
    public class Developer
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public IList<Game> Games { get; set; } = new List<Game>();
    }
}