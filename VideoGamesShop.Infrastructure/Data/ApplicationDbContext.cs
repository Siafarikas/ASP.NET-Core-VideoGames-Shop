using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VideoGamesShop.Infrastructure.Data.Identity;
using VideoGamesShop.Infrastructure.Data.Models;

namespace VideoGamesShop.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Item>()
                .HasKey(x => new { x.UserId, x.GameId });

            base.OnModelCreating(builder);
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<Item> CartItems { get; set; }
    }
}