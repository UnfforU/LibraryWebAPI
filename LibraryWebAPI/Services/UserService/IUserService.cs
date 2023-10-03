using System.Threading.Tasks;

namespace LibraryWebAPI.Services.UserService
{
    public interface IUserService
    {
        Task<List<User>> AddUser(UserDTO user);
        Task<List<User>> DeleteUser(Guid userId);

        User? GetUserByLoginData(LoginDTO login);

        //Task<User?> GetBookById(Guid bookId);
        
        //Task<Book?> UpdateBook(Guid bookId, Book request);
        //Task<List<Book>> DeleteBook(Guid bookId);
    }
}
