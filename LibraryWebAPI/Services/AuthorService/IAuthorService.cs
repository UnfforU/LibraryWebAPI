using LibraryWebAPI.Models.DB;
using Microsoft.AspNetCore.Http.HttpResults;

namespace LibraryWebAPI.Services.AuthorService
{
    public interface IAuthorService
    {
        Task<AuthorDTO> AddAuthorAsync(AuthorDTO author);
        Task<bool> DeleteAuthorAsync(Guid id);

    }
}
