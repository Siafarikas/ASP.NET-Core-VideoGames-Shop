using System.ComponentModel.DataAnnotations;

namespace VideoGamesShop.Infrastructure.Data.Models
{
    using static DataConstants;

    public class Genre
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MinLength(GenreMinLength), MaxLength(GenreMaxLength)]
        public string Label { get; set; }

        [Required]
        [StringLength(GenreDescMaxLength)]
        public string Description { get; set; }

        /*[Required]
        public DateOnly DateFrom { get; set; } = DateOnly.FromDateTime(DateTime.Today);

        [Required]
        public DateOnly? DateTo { get; set; } */

        public IList<Game> Games { get; set; } = new List<Game>();
    }

}