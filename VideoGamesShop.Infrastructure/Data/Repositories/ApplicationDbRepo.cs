
using VideoGamesShop.Infrastructure.Data.Common;

namespace VideoGamesShop.Infrastructure.Data.Repositories
{
    public class ApplicationDbRepo : Repository, IApplicationDbRepo
    {
        public ApplicationDbRepo(ApplicationDbContext context)
        {
            this.Context = context;
        }
    }
}