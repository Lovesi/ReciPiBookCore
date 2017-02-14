using System.Threading.Tasks;
using AspNet.Security.OpenIdConnect.Primitives;
using Microsoft.AspNetCore.Authentication;

namespace ReciPiBook.Services.Authorization
{
    public interface IAuthorizationService
    {
        Task<AuthenticationTicket> SignInAsync(OpenIdConnectRequest request);
    }
}
