using Microsoft.EntityFrameworkCore;
using VideoGamesShop.Infrastructure.Data;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ApiServiceCollectionExtension
    {
        public static IServiceCollection AddApiDbContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            return services;
        }
    }
}
