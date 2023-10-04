using LibraryWebAPI.Models.DB;
using LibraryWebAPI.Services.UserService;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace LibraryWebAPI.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IOptions<AuthOptions> _authOptions;
        private readonly IUserService _userService;
        public AuthService(IOptions<AuthOptions> authOptions, IUserService userService) 
        {
            _authOptions = authOptions;
            _userService = userService;
        }

        public string AuthenticateUser(LoginDTO login)
        {
            var user = _userService.GetUserByLoginData(login);
            if (user == null)
                return String.Empty;

            return GenerateJWT(user);
        }

        private string GenerateJWT(User user)
        {
            var authParams = _authOptions.Value;

            var securityKey = authParams.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                //new Claim("isAdmin", user.IsAdmin == true ? "yes": "no")
            };

            var token = new JwtSecurityToken(
                claims: claims, 
                expires: DateTime.Now.AddSeconds(authParams.TokenLifetime),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
