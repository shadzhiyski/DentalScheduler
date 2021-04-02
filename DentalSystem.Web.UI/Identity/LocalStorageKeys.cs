namespace DentalSystem.Web.UI.Identity
{
    public class LocalStorageKeys
    {
        public class Auth
        {
            public const string AccessToken = nameof(Auth) + "." + nameof(AccessToken);

            public const string AccessTokenExpirationDate = nameof(Auth) + "." + nameof(AccessTokenExpirationDate);
        }

        public class User
        {
            public const string UserName = nameof(User) + "." + nameof(UserName);

            public const string RoleName = nameof(User) + "." + nameof(RoleName);
        }
    }
}