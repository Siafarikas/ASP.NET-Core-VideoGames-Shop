
using VideoGamesShop.Infrastructure.Data.Common;

namespace VideoGamesShop.Infrastructure.Data.Repositories
{
    public class ApplicatioDbRepo : Repository, IApplicatioDbRepo
    {
        public ApplicatioDbRepo(ApplicationDbContext context)
        {
            this.Context = context;
        }
    }
}