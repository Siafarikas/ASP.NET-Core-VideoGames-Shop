using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoGamesShop.Core.Models.Cart
{
    public class CartItemViewModel
    {
        public string GameId { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

    }
}
