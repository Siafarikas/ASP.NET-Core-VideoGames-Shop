using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGamesShop.Infrastructure.Data.Identity;

namespace VideoGamesShop.Infrastructure.Data.Models
{
    public class Purchase
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public string GameId { get; set; }
        public Game Game { get; set; }
    }
}
