
using AutoMapper;
using LibraryWebAPI.Models.DB;
using LibraryWebAPI.Models.DTO;
using LibraryWebAPI.Models.Extra;
using LibraryWebAPI.Services.AuthService;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Cryptography;
using System.Text;

namespace LibraryWebAPI.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly WebLibraryDbContext _context;
        private readonly ICryptographyHelper _cryptoHelper;
        private readonly IMapper _mapper;
        public UserService(WebLibraryDbContext context, ICryptographyHelper cryptoHelper, IMapper mapper)
        {
            this._context = context;
            this._cryptoHelper = cryptoHelper;
            this._mapper = mapper;
        }

        public async Task<UserDTO> AddUserAsync(UserDTO user)
        {
            if (IsUserExist(user.UserName))
            {
                throw new Exception("User already exists!");
            }

            var newUser = _mapper.Map<User>(user);

            newUser.Salt = _cryptoHelper.GenerateSalt();
            newUser.Password = _cryptoHelper.ComputeSHA256(newUser.Password, newUser.Salt);

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserDTO>(newUser);
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user is null)
                return false;

            user.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public User? GetUserByLoginDTO(LoginDTO login)
        {
            var potencialUser = _context.Users
                    .Include(u => u.UserRole)
                    .SingleOrDefault(u => u.UserName == login.UserName && !u.IsDeleted);
 
            if (potencialUser == null)
            {
                return potencialUser;
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

        private bool IsUserExist(string userName)
        {
            return (_context.Users?.Any(e => e.UserName == userName)).GetValueOrDefault();
        }

    }
}
