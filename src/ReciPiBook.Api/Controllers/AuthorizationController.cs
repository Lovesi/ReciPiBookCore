using AspNet.Security.OpenIdConnect.Primitives;
using Microsoft.AspNetCore.Mvc;
using ReciPiBook.Services.Authorization;
using ReciPiBook.Services.Authorization.Exceptions;
using System.Threading.Tasks;

namespace ReciPiBook.Api.Controllers
{
    public class AuthorizationController : Controller
    {
        private IAuthorizationService _authorizationService;

        public AuthorizationController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [HttpPost("~/api/token"), Produces("application/json")]
        public async Task<IActionResult> Exchange(OpenIdConnectRequest request)
        {
            try
            {
                var ticket = await _authorizationService.SignInAsync(request);
                return SignIn(ticket.Principal, ticket.Properties, ticket.AuthenticationScheme);
            }
            catch(BadRequestException ex)
            {
                return BadRequest(ex.Response);
            }
        }
    }
}
