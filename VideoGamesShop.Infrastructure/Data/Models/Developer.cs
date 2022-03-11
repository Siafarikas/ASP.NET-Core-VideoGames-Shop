namespace VideoGamesShop.Data.Models
{
    public class Developer
    {
        public int Id { get; set; }

        public string StudioName { get; set; }

        public IEnumerable<Game> Games { get; set; } = Enumerable.Empty<Game>();
    }
}