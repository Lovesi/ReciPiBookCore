using System;
using AspNet.Security.OpenIdConnect.Primitives;

namespace ReciPiBook.Services.Authorization.Exceptions
{
    public class BadRequestException : Exception
    {
        public OpenIdConnectResponse Response { get; set; }

        public BadRequestException(string error, string errorDescription)
        {
            Response = new OpenIdConnectResponse
            {
                Error = error,
                ErrorDescription = errorDescription
            };
        }
    }
}
