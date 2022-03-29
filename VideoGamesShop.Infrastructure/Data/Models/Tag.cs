using System.ComponentModel.DataAnnotations;

namespace VideoGamesShop.Infrastructure.Data.Models
{
    using static DataConstants;

    public class Tag
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(TagMaxLength)]
        public string Title { get; set; }

        
    }
}