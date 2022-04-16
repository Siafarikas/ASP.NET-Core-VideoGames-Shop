using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VideoGamesShop.Core.Contracts;
using VideoGamesShop.Core.Models.Developer;
using VideoGamesShop.Core.Models.User;
using VideoGamesShop.Core.User.Models;
using VideoGamesShop.Infrastructure.Data.Identity;
using VideoGamesShop.Infrastructure.Data.Models;
using VideoGamesShop.Infrastructure.Data.Repositories;

namespace VideoGamesShop.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IApplicationDbRepo repo;
        private readonly RoleManager<IdentityRole> roleManager;

        private readonly UserManager<ApplicationUser> userManager;
        public UserService(
            IApplicationDbRepo _repo,
            RoleManager<IdentityRole> _roleManager,
            UserManager<ApplicationUser> _userManager)
        {
            repo = _repo;
            roleManager = _roleManager;
            userManager = _userManager;
        }

        public async Task<ApplicationUser> GetUserById(string id)
        {
            return await repo.GetByIdAsync<ApplicationUser>(id);
        }

        public async Task<UserEditViewModel> GetUserForEdit(string id)
        {
            var user = await repo.GetByIdAsync<ApplicationUser>(id);
            return new UserEditViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
            };
        }

        public async Task<IEnumerable<UserListViewModel>> GetUsers()
        {
            return await repo.All<ApplicationUser>()
                .Select(u => new UserListViewModel()
                {
                    Id = u.Id,
                    Email = u.Email,
                    Name = $"{u.FirstName} {u.LastName}"
                })
                .ToListAsync();
        }

        public async Task<UserProfileViewModel> GetUserProfileInfo(string id)
        {
            return await repo.All<ApplicationUser>()
                .Where(u => u.Id == id)
                .Select(u => new UserProfileViewModel()
                {
                    Id = u.Id,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateUser(UserEditViewModel model)
        {
            bool result = false;
            var user = await repo.GetByIdAsync<ApplicationUser>(model.Id);

            if (user != null)
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;

                await repo.SaveChangesAsync();
                result = true;
            }

            return result;
        }

        public async Task AddMoneyToWallet(string userId, decimal amount)
        {
            var user = await repo.GetByIdAsync<ApplicationUser>(userId);
            user.Wallet += amount;

            await repo.SaveChangesAsync();
        }

        // Developer methods
        public async Task<string> GetDeveloperIdByUserId(string userId)
        {
            var user = await repo.GetByIdAsync<ApplicationUser>(userId);

            var dev = await repo.All<Developer>()
                .Where(d => d.UserId == userId)
                .FirstOrDefaultAsync();

            return dev.Id;
        }

        public async Task<bool> UserBecomesDeveloper(string userId)
        {
            if (userId == null)
            {
                return false;
            }

            var user = await repo.GetByIdAsync<ApplicationUser>(userId);

            if (user == null)
            {
                return false;
            }

            Developer developer = new Developer()
            {
                Id = Guid.NewGuid().ToString(),
                UserId = userId,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
            await userManager.AddToRolesAsync(user, new List<string> { "Developer" });

            await repo.AddAsync(developer);
            await repo.SaveChangesAsync();

            return true;
        }

        public async Task<StatisticsPageViewModel> GetStatistics(string userId)
        {
            var model = new StatisticsPageViewModel();

            string developerId = await GetDeveloperIdByUserId(userId);
            if (developerId == null)
            {
                return null;
            }

            model.SalesCount = (int)await repo.All<Game>()
                .Where(g => g.DeveloperId == developerId)
                .Select(g => g.Sales)
                .SumAsync();

            var allGamesOfDev = await repo.All<Game>()
                .Where(g => g.DeveloperId == developerId)
                .ToListAsync();

            decimal? revenue = 0;
            foreach (var game in allGamesOfDev)
            {
                revenue += game.Sales * game.Price;
            }
            model.Revenue = revenue;


            model.Sales = await (from purchase in repo.All<Purchase>()
                                 from game in repo.All<Game>().Where(g => g.Id == purchase.GameId && g.DeveloperId == developerId)
                                 from user in repo.All<ApplicationUser>().Where(u => u.Id == purchase.UserId)
                                 select new SalesViewModel()
                                 {
                                     CustomerName = $"{user.FirstName} {user.LastName}",
                                     GameTitle = game.Title,
                                     GamePrice = game.Price
                                 })
                                 .ToListAsync();

            model.Sales = model.Sales
                .Reverse()
                .Take(5);

            return model;
        }

    }
}
