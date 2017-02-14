using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReciPiBook.Api.Extensions;
using ReciPiBook.Dtos;
using ReciPiBook.Services.User;

namespace ReciPiBook.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly IUserRegistrationService _userRegistrationService;

        public AccountController(IUserRegistrationService userRegistrationService)
        {
            _userRegistrationService = userRegistrationService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegistrationData model)
        {
            IdentityResult result = null;

            if (ModelState.IsValid)
            {
                result = await _userRegistrationService.RegisterNewUser(model);
                if (result.Succeeded)
                    return Ok();                    
            }

            return BadRequest(ModelState.AddErrors(result));
        }
    }
}
