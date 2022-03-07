namespace VideoGamesShop.Data.Models
{
    public class Game
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public IEnumerable<Genre> Genres { get; set; }

        public IEnumerable<Tag> Tags { get; set; }

        public DateTime ReleaseDate { get; set; }

        public Developer Developer { get; set; }
    }
}
