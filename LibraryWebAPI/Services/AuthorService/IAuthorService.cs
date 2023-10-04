using LibraryWebAPI.Models.DB;

namespace LibraryWebAPI.Services.AuthorService
{
    public interface IAuthorService
    {
        Task<Author> AddAuthor(Author author);

    }
}
