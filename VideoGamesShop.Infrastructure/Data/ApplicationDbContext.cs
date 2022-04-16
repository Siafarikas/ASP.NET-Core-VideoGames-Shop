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

            builder.Entity<Purchase>()
            .HasKey(p => new { p.UserId, p.GameId });

            builder.Entity<Purchase>()
            .HasOne(p => p.User)
            .WithMany(g => g.Games)
            .HasForeignKey(p => p.UserId);

            builder.Entity<Purchase>()
                .HasOne(p => p.Game)
                .WithMany(u => u.Users)
                .HasForeignKey(p => p.GameId);

            base.OnModelCreating(builder);
        }

        public DbSet<Game> Games { get; init; }
        public DbSet<Genre> Genres { get; init; }
        public DbSet<Developer> Developers { get; init; }
        public DbSet<Item> CartItems { get; init; }
        public DbSet<Purchase> Purchases { get; init; }
    }
}