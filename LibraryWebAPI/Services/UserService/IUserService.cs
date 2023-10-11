namespace LibraryWebAPI.Services.UserService
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetUsersAsync();
        Task<UserDTO> AddUserAsync(UserDTO user);
        Task<bool> DeleteUserAsync(Guid id);
        User? GetUserByLoginDTO(LoginDTO login);
    }
}
