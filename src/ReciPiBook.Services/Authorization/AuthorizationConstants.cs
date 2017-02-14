namespace ReciPiBook.Services.Authorization
{
    public static class AuthorizationConstants
    {
        public static class Errors
        {
            public const string NotAllowed = "The specified user is not allowed to sign in.";
            public const string GrantType = "The specified grant type is not supported.";
            public const string UserPass = "The username/password couple is invalid.";
        }
    }
}
