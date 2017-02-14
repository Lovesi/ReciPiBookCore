using System;
using AspNet.Security.OpenIdConnect.Primitives;

namespace ReciPiBook.Services.Authorization.Exceptions
{
    public class BadRequestException : Exception
    {
        public OpenIdConnectResponse Response { get; set; }

        public BadRequestException(string Error, string ErrorDescription)
        {
            Response = new OpenIdConnectResponse
            {
                Error = Error,
                ErrorDescription = ErrorDescription
            };
        }
    }
}
