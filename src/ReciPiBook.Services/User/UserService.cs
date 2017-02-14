using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ReciPiBook.Dtos;

namespace ReciPiBook.Services.User
{
    public class UserService : IUserRegistrationService
    {
        private readonly UserManager<Entities.ApplicationUser> _userManager;

        public UserService(UserManager<Entities.ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> RegisterNewUser(RegistrationData registrationData)
        {
            var user = new Entities.ApplicationUser { UserName = registrationData.Email, Email = registrationData.Email };
            return await _userManager.CreateAsync(user, registrationData.Password);
        }
    }
}
