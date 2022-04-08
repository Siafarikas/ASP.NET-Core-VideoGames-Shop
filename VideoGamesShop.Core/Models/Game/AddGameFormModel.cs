using System.ComponentModel.DataAnnotations;

namespace VideoGamesShop.Core.Models.Game
{
    using static Infrastructure.Data.DataConstants;
    public class AddGameFormModel
    {
        [Required]
        [StringLength(
            GameTitleMaxLength, 
            MinimumLength = GameTitleMinLength,
            ErrorMessage = "The field Title must be a string with a minimum length of {2} and a maximum length of {1}.")]
        public string Title { get; init; }

        [Required]
        [Range(
            GameMinPrice,
            GameMaxPrice,
            ErrorMessage = "The field Price must be a number between {1} and {2}.")]
        public decimal Price { get; init; }

        [Required]
        [StringLength(
            GameDescMaxLength,
            MinimumLength = GameDescMinLength,
            ErrorMessage = "The field Description must be a string with a minimum length between {2} and {1}.")]
        public string Description { get; init; }


        [Required]
        [RegularExpression(
            @"^\d{4}\-(0[1-9]|1[012])\-(0[1-9]|[12][0-9]|3[01])$",
            ErrorMessage = "Date must be in format \"yyyy-mm-dd\". Ex. \"2012-1-2\" is not valid, must be \"2012-01-02\".")]
        public string ReleaseDate { get; init; }

        [Required]
        [Url(ErrorMessage = "Not a valid Url.")]
        public string ImageUrl { get; init; }

        [Display(Name = "Genre")]
        public string GenreId { get; init; }
        public IEnumerable<GameGenreModel> Genres { get; set; }
    }
}
