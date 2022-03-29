using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoGamesShop.Core.Models
{
    public class GameListViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }

        public decimal Price { get; set; }

        public string Genre { get; set; }

        public string ImageUrl { get; set; }
    }
}
