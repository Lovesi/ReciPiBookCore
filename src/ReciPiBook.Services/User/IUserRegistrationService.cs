using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ReciPiBook.Dtos;

namespace ReciPiBook.Services.User
{
    public interface IUserRegistrationService
    {
        Task<IdentityResult> RegisterNewUser(RegistrationData registrationData);
    }
}