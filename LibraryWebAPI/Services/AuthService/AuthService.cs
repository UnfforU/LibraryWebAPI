using LibraryWebAPI.Models.DB;
using LibraryWebAPI.Services.UserService;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

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
            var user = _userService.GetUserByLoginData(login);
            if (user == null)
                return String.Empty;

            return _cryptoHelper.GenerateJWT(user);
        }
    }
}
