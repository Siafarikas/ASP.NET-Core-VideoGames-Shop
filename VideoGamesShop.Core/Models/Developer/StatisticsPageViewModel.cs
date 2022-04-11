using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGamesShop.Infrastructure.Data.Models;

namespace VideoGamesShop.Core.Models.Developer
{
    public class StatisticsPageViewModel
    {
        public int SalesCount { get; set; }

        public decimal? Revenue { get; set; }

        //public int Customers { get; set; }

        public ICollection<SalesViewModel> Sales { get; set; } 
    }
}
