﻿using Microsoft.EntityFrameworkCore;
using VideoGamesShop.Core.Contracts;
using VideoGamesShop.Core.Models.User;
using VideoGamesShop.Core.User.Models;
using VideoGamesShop.Infrastructure.Data.Identity;
using VideoGamesShop.Infrastructure.Data.Models;
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

        public async Task<string> GetDeveloperIdByUserId(string userId)
        {
            var user = await repo.GetByIdAsync<ApplicationUser>(userId);

            var dev = await repo.All<Developer>()
                .Where(d => d.UserId == userId)
                .FirstOrDefaultAsync();

            return dev.Id;
        }

    }
}
