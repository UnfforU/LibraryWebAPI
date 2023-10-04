
using LibraryWebAPI.Models.DB;
using LibraryWebAPI.Models.Extra;
using LibraryWebAPI.Services.AuthService;
using System.Security.Cryptography;
using System.Text;

namespace LibraryWebAPI.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly WebLibraryDbContext _context;
        private readonly ICryptographyHelper _cryptoHelper;

        public UserService(WebLibraryDbContext context, ICryptographyHelper cryptoHelper)
        {
            this._context = context;
            this._cryptoHelper = cryptoHelper;
        }

        public async Task<List<User>> AddUser(UserDTO userDTO)
        {

            //!Нужна проверка на то, есть ли пользователь уже с таким же именем


            var _salt = _cryptoHelper.GenerateSalt();
            var newUser = new User
            {
                UserId = Guid.NewGuid(),
                UserName = userDTO.UserName,
                Salt = _salt,
                Password = _cryptoHelper.ComputeSHA256(userDTO.Password, _salt),
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
            else if (potencialUser.Password == _cryptoHelper.ComputeSHA256(login.Password, potencialUser.Salt)) 
            {
                return potencialUser;
            }
            else
            {
                return null;
            }
        }
    }
}
