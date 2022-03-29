using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideoGamesShop.Infrastructure.Data.Models
{
    using static DataConstants;
    public class Game
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();


        [Required]
        [MinLength(GameTitleMinLength), MaxLength(GameTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [DataType("money")]
        public decimal Price { get; set; }

        [Required]
        [Range(GameReleaseDateMinYearLength, GameReleaseDateMaxYearLength)]
        public DateTime ReleaseDate { get; set; }

        [Required]
        [MinLength(GameDescMinLength), MaxLength(GameDescMaxLength)]
        public string Description { get; set; }

        [Required]
        [MaxLength(250)]
        public string ImageUrl { get; set; }

        public int? Sales { get; init; } = 0;


        public string DeveloperId { get; set; }

        [ForeignKey(nameof(DeveloperId))]
        public Developer Developer { get; set; }


        public string GenreId { get; set; } 

        [ForeignKey(nameof(GenreId))]
        public Genre Genre { get; set; }


        public IList<Tag> Tags { get; set; } = new List<Tag>();

    }
}
