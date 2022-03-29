using Microsoft.EntityFrameworkCore;
using VideoGamesShop.Core.Contracts;
using VideoGamesShop.Core.User.Models;
using VideoGamesShop.Infrastructure.Data.Identity;
using VideoGamesShop.Infrastructure.Data.Repositories;

namespace VideoGamesShop.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IApplicatioDbRepo repo;

        public UserService(IApplicatioDbRepo _repo)
        {
            repo = _repo;
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

    }
}
