using System.Security.Cryptography;
using System.Text;

namespace LibraryWebAPI.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly LibraryContext _context;
        public UserService(LibraryContext context)
        {
            this._context = context;
        }

        public async Task<List<User>> AddUser(UserDTO userDTO)
        {
            var newUser = new User
            {
                UserId = Guid.NewGuid(),
                UserName = userDTO.UserName,
                Password = ComputeSHA256(userDTO.Password),
                IsAdmin = userDTO.IsAdmin,
                IsDeleted = false
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

        public User? GetUserByLoginData(Login login) =>
            _context.Users.SingleOrDefault(u =>
                (u.IsDeleted == false)
                && (u.UserName == login.UserName) 
                && (u.Password == ComputeSHA256(login.Password)));
    
           

        private string ComputeSHA256(string input)
        {
            string hash = String.Empty;
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                foreach (byte b in hashValue) 
                {
                    hash += $"{b:X2}";
                }
            }
            return hash;
        }
    }
}
