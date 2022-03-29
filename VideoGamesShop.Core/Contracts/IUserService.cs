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
    }
}
