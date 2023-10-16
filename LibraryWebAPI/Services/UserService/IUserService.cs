namespace LibraryWebAPI.Services.UserService
{
    using LibraryWebAPI.Models.DB;
    public interface IUserService
    {
        Task<List<UserDTO>> GetUsersAsync();
        Task<UserDTO> AddUserAsync(UserDTO user);
        Task<bool> DeleteUserAsync(Guid id);
        User? GetUserByLoginDTO(LoginDTO login);
    }
}
