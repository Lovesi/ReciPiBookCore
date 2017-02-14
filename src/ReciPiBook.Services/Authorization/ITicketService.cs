using System.Threading.Tasks;
using AspNet.Security.OpenIdConnect.Primitives;
using Microsoft.AspNetCore.Authentication;

namespace ReciPiBook.Services.Authorization
{
    public interface ITicketService
    {
        Task<AuthenticationTicket> CreateTicketAsync(OpenIdConnectRequest request, Entities.ApplicationUser user);
    }
}
