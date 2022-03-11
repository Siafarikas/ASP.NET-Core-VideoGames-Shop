using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideoGamesShop.Data.Models
{
    using static DataConstants;
    public class Game
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(GameTitleMaxLength)]
        public string Title { get; set; }

        [Required]
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

        public int DeveloperId { get; set; }

        [Required]
        [ForeignKey(nameof(DeveloperId))]
        public Developer Developer { get; set; }

        public IEnumerable<Genre> Genres { get; set; }

        public IEnumerable<Tag> Tags { get; set; }

    }
}
