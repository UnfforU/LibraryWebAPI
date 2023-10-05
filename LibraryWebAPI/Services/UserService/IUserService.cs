using System.Threading.Tasks;
using LibraryWebAPI.Models.DB;

namespace LibraryWebAPI.Services.UserService
{
    public interface IUserService
    {
        Task<UserDTO> AddUserAsync(UserDTO user);
        Task<bool> DeleteUserAsync(Guid id);
        User? GetUserByLoginDTO(LoginDTO login);
    }
}
