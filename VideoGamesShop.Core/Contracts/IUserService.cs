using VideoGamesShop.Core.Models.Developer;
using VideoGamesShop.Core.Models.User;
using VideoGamesShop.Core.User.Models;
using VideoGamesShop.Infrastructure.Data.Identity;

namespace VideoGamesShop.Core.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserListViewModel>> GetUsers(); 

        Task<UserEditViewModel> GetUserForEdit(string id);

        Task<bool> UpdateUser(UserEditViewModel model);

        Task<ApplicationUser> GetUserById(string id);

        Task<UserProfileViewModel> GetUserProfileInfo(string id);

        Task AddMoneyToWallet(string userId, decimal amount);

        Task<string> GetDeveloperIdByUserId(string userId);

        Task<bool> UserBecomesDeveloper(string userId);

        Task<StatisticsPageViewModel> GetStatistics(string userId);
    }
}
