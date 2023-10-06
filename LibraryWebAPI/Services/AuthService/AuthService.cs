using LibraryWebAPI.Services.UserService;

namespace LibraryWebAPI.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ICryptographyHelper _cryptoHelper;
        public AuthService(IUserService userService, ICryptographyHelper cryptoHelper) 
        {
            this._userService = userService;
            this._cryptoHelper = cryptoHelper;
        }

        public string AuthenticateUser(LoginDTO login)
        {
            var user = _userService.GetUserByLoginDTO(login);
            if (user == null)
                return String.Empty;

            return _cryptoHelper.GenerateJWT(user);
        }
    }
}
