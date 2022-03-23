using System.ComponentModel.DataAnnotations;

namespace VideoGamesShop.Infrastructure.Data.Models
{
    using static DataConstants;

    public class Tag
    {
        [Key]
        public int Id { get; set; } 

        [Required]
        [StringLength(TagMaxLength)]
        public string Title { get; set; }

        
    }
}