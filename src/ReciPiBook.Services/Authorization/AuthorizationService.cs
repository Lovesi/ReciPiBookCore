using System.Collections.Generic;
using System.Threading.Tasks;
using AspNet.Security.OpenIdConnect.Primitives;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using OpenIddict.Core;
using OpenIddict.Models;
using ReciPiBook.Entities;
using ReciPiBook.Services.Authorization.Exceptions;

namespace ReciPiBook.Services.Authorization
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly OpenIddictApplicationManager<OpenIddictApplication> _applicationManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITicketService _ticketService;
        private delegate Task<bool> ValidateMethod(ApplicationUser user, string s = null);
        private List<ValidateMethod> _validationMethods;

        public AuthorizationService(OpenIddictApplicationManager<OpenIddictApplication> applicationManager,
                                    SignInManager<ApplicationUser> signInManager,
                                    UserManager<ApplicationUser> userManager,
                                    ITicketService ticketService)
        {
            _applicationManager = applicationManager;
            _signInManager = signInManager;
            _userManager = userManager;
            _ticketService = ticketService;
        }

        public async Task<AuthenticationTicket> SignInAsync(OpenIdConnectRequest request)
        {
            if (!request.IsPasswordGrantType())
                throw new BadRequestException(OpenIdConnectConstants.Errors.InvalidGrant,
                                              AuthorizationConstants.Errors.GrantType);

            var user = await GetUser(request.Username);

            BuildValidationFunctions();
            await RunValidations(_validationMethods, user, request.Password);
            await ResetLockout(user);

            return await _ticketService.CreateTicketAsync(request, user);
        }

        #region Helpers

        private void BuildValidationFunctions()
        {
            _validationMethods = new List<ValidateMethod>();

            //Check if user can sign-in
            _validationMethods.Add(async (user, s) =>
            {
                if (!await _signInManager.CanSignInAsync(user))
                    throw new BadRequestException(OpenIdConnectConstants.Errors.InvalidGrant,
                                                  AuthorizationConstants.Errors.NotAllowed);
                return true;
            });

            //Reject the token request if two-factor authentication has been enabled by the user.
            _validationMethods.Add(async (user, s) =>
            {
                if (_userManager.SupportsUserTwoFactor && await _userManager.GetTwoFactorEnabledAsync(user))
                    throw new BadRequestException(OpenIdConnectConstants.Errors.InvalidGrant,
                                                  AuthorizationConstants.Errors.NotAllowed);
                return true;
            });

            //Ensure the user is not already locked out.
            _validationMethods.Add(async (user, s) =>
            {
                if (_userManager.SupportsUserLockout && await _userManager.IsLockedOutAsync(user))
                    throw new BadRequestException(OpenIdConnectConstants.Errors.InvalidGrant,
                                                  AuthorizationConstants.Errors.UserPass);
                return true;
            });

            //Ensure the password is valid.
            _validationMethods.Add(async (user, s) =>
            {
                if (!await _userManager.CheckPasswordAsync(user, s))
                {
                    if (_userManager.SupportsUserLockout)
                        await _userManager.AccessFailedAsync(user);

                    throw new BadRequestException(OpenIdConnectConstants.Errors.InvalidGrant,
                                                  AuthorizationConstants.Errors.UserPass);
                }
                return true;
            });
        }

        private async Task<ApplicationUser> GetUser(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                throw new BadRequestException(OpenIdConnectConstants.Errors.InvalidGrant,
                                              AuthorizationConstants.Errors.UserPass);
            return user;
        }

        private async Task ResetLockout(ApplicationUser user)
        {
            if (_userManager.SupportsUserLockout)
                await _userManager.ResetAccessFailedCountAsync(user);
        }

        private async Task<bool> RunValidations(List<ValidateMethod> validations, ApplicationUser user, string password)
        {
            foreach(var validation in validations)
            {
                if (await validation(user, password))
                    continue;
                else
                    return false;
            }

            return true;
        }

        #endregion
    }
}
