using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LibraryWebAPI.Helpers
{
    public class CryptographyHelper : ICryptographyHelper
    {
        private const int MAXIMUM_SALT_LENGTH = 8;
        private readonly IOptions<AuthOptions> _authOptions;

        public CryptographyHelper(IOptions<AuthOptions> authOptions)
        {
            _authOptions = authOptions;
        }

        public string ComputeSHA256(string input, string salt)
        {
            string hash = String.Empty;
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(String.Concat(input, salt)));
                foreach (byte b in hashValue)
                {
                    hash += $"{b:X2}";
                }
            }
            return hash;
        }

        public string GenerateSalt()
        {
            var salt = new byte[MAXIMUM_SALT_LENGTH];
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }

        public string GenerateJWT(User user)
        {
            var authParams = _authOptions.Value;

            var securityKey = authParams.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim("role", user.UserRoleId.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: authParams.Issuer,
                audience: authParams.Audience,
                claims: claims,
                expires: DateTime.Now.AddSeconds(authParams.TokenLifetime),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
