using LibraryWebAPI.Models.DB;
using LibraryWebAPI.Models.Extra;
using System.Security.Cryptography;
using System.Text;

namespace LibraryWebAPI.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly WebLibraryDbContext _context;
      
        public UserService(WebLibraryDbContext context)
        {
            this._context = context;
        }

        public async Task<List<User>> AddUser(UserDTO userDTO)
        {

            //!Нужна проверка на то, есть ли пользователь уже с таким же именем


            var _salt = GenerateSalt();
            var newUser = new User
            {
                UserId = Guid.NewGuid(),
                UserName = userDTO.UserName,
                Salt = _salt,
                Password = ComputeSHA256WithSalt(userDTO.Password, _salt),
                Role = new UserRole() { UserRoleId = Guid.NewGuid(), Name = "DefaultUser", RoleIndex = EnumUserRoles.DefaultUser },
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return await _context.Users.ToListAsync();
        }

        public async Task<List<User>> DeleteUser(Guid userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user is null)
                return null;

            user.IsDeleted = true;
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return await _context.Users.Where(u => u.IsDeleted != true).ToListAsync();
        }

        public User? GetUserByLoginData(LoginDTO login)
        {
            var potencialUser = _context.Users.SingleOrDefault(u => u.UserName == login.UserName && !u.IsDeleted);

            if(potencialUser == null)
            {
                return null;
            }
            else if (potencialUser.Password == ComputeSHA256WithSalt(login.Password, potencialUser.Salt)) 
            {
                return potencialUser;
            }
            else
            {
                return null;
            }
        }
            
    
           

        private string ComputeSHA256WithSalt(string input, string salt)
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

        private string GenerateSalt()
        {
            var maximumSaltLength = 8;
            var salt = new byte[maximumSaltLength];

            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);

            }
            return Convert.ToBase64String(salt);

        }
    }
}
