using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace VideoGamesShop.Infrastructure.Data.Identity
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        [StringLength(50)]
        public string FirstName { get; set; }

        [PersonalData]
        [StringLength(50)]
        public string LastName { get; set; }

        public decimal Wallet { get; set; } = 0;

    }
}
