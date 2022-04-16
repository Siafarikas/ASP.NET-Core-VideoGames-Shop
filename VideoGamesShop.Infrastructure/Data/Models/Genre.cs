using System.ComponentModel.DataAnnotations;

namespace VideoGamesShop.Infrastructure.Data.Models
{
    using static DataConstants;

    public class Genre
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MinLength(GenreMinLength), MaxLength(GenreMaxLength)]
        public string Title { get; set; }

    }

}